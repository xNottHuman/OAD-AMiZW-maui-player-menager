
using MauiApp3.Data;
using MauiApp3.Model;
using System.Linq;

namespace MauiApp3.Commands
{
    public class AddPlayerCommandHandler
    {
        private readonly PlayerRepository _playerRepository;

        public AddPlayerCommandHandler(PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public void Handle(AddPlayerCommand command)
        {
            int newId = _playerRepository.Players
                .Select(p => p.Id)
                .DefaultIfEmpty(0)
                .Max() + 1;

            Player player = new Player
            {
                Id = newId,
                Name = command.Name,
                Level = command.Level
            };

            _playerRepository.Players.Add(player);
        }
    }
}

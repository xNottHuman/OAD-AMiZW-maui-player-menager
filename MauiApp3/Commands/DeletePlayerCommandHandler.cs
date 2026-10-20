using MauiApp3.Data;
using MauiApp3.Model;
using System.Linq;

namespace MauiApp3.Commands
{
    /*************************************************************************
     * nazwa klasy:
     * DeletePlayerCommandHandler
     *
     * opis:
     * Klasa obsługuje polecenia usunięcia gracza.
     * Wyszukuje gracza w repozytorium i usuwa go, jeśli istnieje.
     *
     * pola / właściwości:
     * _playerRepository, PlayerRepository, repozytorium zawierające graczy
     *
     * autor:
     * Eryk Sikora
     *************************************************************************/
    public class DeletePlayerCommandHandler
    {
        private readonly PlayerRepository _playerRepository;

        /*************************************************************************
         * nazwa funkcji:
         * DeletePlayerCommandHandler
         *
         * parametry wejściowe:
         * playerRepository, PlayerRepository, repozytorium graczy
         *
         * wartość zwracana:
         * brak
         *
         * opis funkcji:
         * Tworzy handler korzystający z podanego repozytorium graczy.
         *
         * autor:
         * Eryk Sikora
         *************************************************************************/
        public DeletePlayerCommandHandler(
            PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        /*************************************************************************
         * nazwa funkcji:
         * Handle
         *
         * parametry wejściowe:
         * command, DeletePlayerCommand, polecenie z Id gracza do usunięcia
         *
         * wartość zwracana:
         * bool, informacja czy gracz został znaleziony i usunięty
         *
         * opis funkcji:
         * Wyszukuje gracza o podanym Id w repozytorium i usuwa go.
         * Zwraca false, jeżeli gracz nie istnieje.
         *
         * autor:
         * Eryk Sikora
         *************************************************************************/
        public bool Handle(DeletePlayerCommand command)
        {
            Player? player = _playerRepository.Players
                .FirstOrDefault(p => p.Id == command.Id);

            if (player == null)
            {
                return false;
            }

            _playerRepository.Players.Remove(player);
            return true;
        }
    }
}

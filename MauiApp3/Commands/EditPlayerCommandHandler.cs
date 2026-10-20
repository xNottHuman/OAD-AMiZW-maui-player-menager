using MauiApp3.Data;
using MauiApp3.Model;
using System.Linq;

namespace MauiApp3.Commands
{
    /*************************************************************************
     * nazwa klasy:
     * EditPlayerCommandHandler
     *
     * opis:
     * Klasa obsługuje polecenia edycji gracza.
     * Wyszukuje gracza w repozytorium i aktualizuje jego nazwę oraz poziom.
     *
     * pola / właściwości:
     * _playerRepository, PlayerRepository, repozytorium zawierające graczy
     *
     * autor:
     * Eryk Sikora
     *************************************************************************/
    public class EditPlayerCommandHandler
    {
        private readonly PlayerRepository _playerRepository;

        /*************************************************************************
         * nazwa funkcji:
         * EditPlayerCommandHandler
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
        public EditPlayerCommandHandler(PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        /*************************************************************************
         * nazwa funkcji:
         * Handle
         *
         * parametry wejściowe:
         * command, EditPlayerCommand, polecenie z Id i nowymi danymi gracza
         *
         * wartość zwracana:
         * bool, informacja czy gracz został znaleziony i zmieniony
         *
         * opis funkcji:
         * Wyszukuje gracza o podanym Id i aktualizuje jego nazwę oraz poziom.
         * Zwraca false, jeżeli gracz nie istnieje.
         *
         * autor:
         * Eryk Sikora
         *************************************************************************/
        public bool Handle(EditPlayerCommand command)
        {
            Player? player = _playerRepository.Players
                .FirstOrDefault(player => player.Id == command.Id);

            if (player == null)
            {
                return false;
            }

            player.Name = command.Name;
            player.Level = command.Level;
            return true;
        }
    }
}
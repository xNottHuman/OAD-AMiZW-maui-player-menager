
using MauiApp3.Commands;
using MauiApp3.Model;
using MauiApp3.Queries;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiApp3.ViewModel
{
    /*************************************************************************
     * nazwa klasy:
     * MainViewModel
     *
     * opis:
     * Klasa udostępnia dane i polecenia widoku głównego aplikacji.
     * Łączy interfejs z handlerami operacji na graczach.
     *
     * pola / właściwości:
     * Players, ObservableCollection<Player>, gracze wyświetlani na liście
     * PlayerName, string, nazwa nowego gracza
     * PlayerLevel, string, poziom nowego gracza
     * PlayerId, string, Id gracza wyszukiwanego
     * PlayerIdToDelete, string, Id gracza usuwanego
     * PlayerIdToEdit, string, Id gracza edytowanego
     * PlayerNameToEdit, string, nowa nazwa edytowanego gracza
     * PlayerLevelToEdit, string, nowy poziom edytowanego gracza
     * SearchResult, string, komunikat z wyniku wyszukiwania
     * DeleteResult, string, komunikat z wyniku usuwania
     * EditResult, string, komunikat z wyniku edycji
     * AddPlayerCommandButton, ICommand, polecenie dodania gracza
     * FindPlayerCommandButton, ICommand, polecenie wyszukania gracza
     * DeletePlayerCommandButton, ICommand, polecenie usunięcia gracza
     * EditPlayerCommandButton, ICommand, polecenie edycji gracza
     *
     * autor:
     * Eryk Sikora
     *************************************************************************/
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly GetPlayerByIdQueryHandler _getPlayerByIdQueryHandler;
        private readonly GetPlayersQueryHandler _getPlayersQueryHandler;
        private readonly AddPlayerCommandHandler _addPlayerCommandHandler;
        private readonly DeletePlayerCommandHandler _deletePlayerCommandHandler;
        private readonly EditPlayerCommandHandler _editPlayerCommandHandler;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Player> Players { get; } = new();

        public string PlayerName { get; set; } = "";
        public string PlayerLevel { get; set; } = "";
        public string PlayerId { get; set; } = "";
        public string PlayerIdToDelete { get; set; } = "";
        public string PlayerIdToEdit { get; set; } = "";
        public string PlayerNameToEdit { get; set; } = "";
        public string PlayerLevelToEdit { get; set; } = "";

        private string _searchResult = "";
        private string _deleteResult = "";
        private string _editResult = "";

        public string SearchResult
        {
            get => _searchResult;
            set
            {
                _searchResult = value;
                OnPropertyChanged();
            }
        }

        public string EditResult
        {
            get => _editResult;
            set
            {
                _editResult = value;
                OnPropertyChanged();
            }
        }

        public string DeleteResult
        {
            get => _deleteResult;
            set
            {
                _deleteResult = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddPlayerCommandButton { get; }
        public ICommand FindPlayerCommandButton { get; }
        public ICommand DeletePlayerCommandButton { get; }
        public ICommand EditPlayerCommandButton { get; }

        /*************************************************************************
         * nazwa funkcji:
         * MainViewModel
         *
         * parametry wejściowe:
         * getPlayerByIdQueryHandler, GetPlayerByIdQueryHandler, handler wyszukiwania gracza
         * getPlayersQueryHandler, GetPlayersQueryHandler, handler pobierania listy graczy
         * addPlayerCommandHandler, AddPlayerCommandHandler, handler dodawania gracza
         * deletePlayerCommandHandler, DeletePlayerCommandHandler, handler usuwania gracza
         * editPlayerCommandHandler, EditPlayerCommandHandler, handler edycji gracza
         *
         * wartość zwracana:
         * brak
         *
         * opis funkcji:
         * Inicjalizuje handlery i polecenia widoku, a następnie pobiera graczy.
         *
         * autor:
         * Eryk Sikora
         *************************************************************************/
        public MainViewModel(
            GetPlayerByIdQueryHandler getPlayerByIdQueryHandler,
            GetPlayersQueryHandler getPlayersQueryHandler,
            AddPlayerCommandHandler addPlayerCommandHandler,
            DeletePlayerCommandHandler deletePlayerCommandHandler,
            EditPlayerCommandHandler editPlayerCommandHandler)
        {
            _getPlayerByIdQueryHandler = getPlayerByIdQueryHandler;
            _getPlayersQueryHandler = getPlayersQueryHandler;
            _addPlayerCommandHandler = addPlayerCommandHandler;
            _deletePlayerCommandHandler = deletePlayerCommandHandler;
            _editPlayerCommandHandler = editPlayerCommandHandler;

            AddPlayerCommandButton = new Command(AddPlayer);
            FindPlayerCommandButton = new Command(FindPlayer);
            DeletePlayerCommandButton = new Command(DeletePlayer);
            EditPlayerCommandButton = new Command(EditPlayer);

            LoadPlayers();
        }

        /*************************************************************************
         * nazwa funkcji:
         * AddPlayer
         *
         * parametry wejściowe:
         * brak
         *
         * wartość zwracana:
         * brak
         *
         * opis funkcji:
         * Sprawdza poziom wpisany przez użytkownika, przekazuje polecenie
         * dodania do handlera i odświeża listę graczy.
         *
         * autor:
         * Eryk Sikora
         *************************************************************************/
        private void AddPlayer()
        {
            if (!int.TryParse(PlayerLevel, out int playerLevel))
            {
                return;
            }

            AddPlayerCommand command =
                new AddPlayerCommand(PlayerName, playerLevel);

            _addPlayerCommandHandler.Handle(command);

            LoadPlayers();
        }

        /*************************************************************************
         * nazwa funkcji:
         * FindPlayer
         *
         * parametry wejściowe:
         * brak
         *
         * wartość zwracana:
         * brak
         *
         * opis funkcji:
         * Sprawdza Id wpisane przez użytkownika i wyszukuje gracza przez handler.
         * Wynik lub komunikat o błędzie udostępnia w SearchResult.
         *
         * autor:
         * Eryk Sikora
         *************************************************************************/
        private void FindPlayer()
        {
            if (!int.TryParse(PlayerId, out int playerId))
            {
                SearchResult = "Niepoprawne ID";
                return;
            }

            GetPlayerByIdQuery query =
                new GetPlayerByIdQuery(playerId);

            Player? player =
                _getPlayerByIdQueryHandler.Handle(query);

            if (player == null)
            {
                SearchResult = "Nie znaleziono gracza";
                return;
            }

            SearchResult = $"{player.Name} - poziom {player.Level}";
        }

        /*************************************************************************
         * nazwa funkcji:
         * DeletePlayer
         *
         * parametry wejściowe:
         * brak
         *
         * wartość zwracana:
         * brak
         *
         * opis funkcji:
         * Sprawdza Id wpisane przez użytkownika, przekazuje polecenie do handlera
         * usuwania, wyświetla wynik i odświeża listę graczy po powodzeniu.
         *
         * autor:
         * Eryk Sikora
         *************************************************************************/
        private void DeletePlayer()
        {
            if (!int.TryParse(PlayerIdToDelete, out int playerId)
                || playerId <= 0)
            {
                DeleteResult = "Podaj poprawne ID gracza";
                return;
            }

            DeletePlayerCommand command =
                new DeletePlayerCommand(playerId);

            bool deleted =
                _deletePlayerCommandHandler.Handle(command);

            if (!deleted)
            {
                DeleteResult = "Nie znaleziono gracza";
                return;
            }

            LoadPlayers();

            DeleteResult = "Gracz został usunięty";
            PlayerIdToDelete = "";
            OnPropertyChanged(nameof(PlayerIdToDelete));
        }

        /*************************************************************************
         * nazwa funkcji:
         * EditPlayer
         *
         * parametry wejściowe:
         * brak
         *
         * wartość zwracana:
         * brak
         *
         * opis funkcji:
         * Waliduje Id, nazwę i poziom, przekazuje polecenie do handlera edycji,
         * informuje o wyniku i odświeża listę graczy po powodzeniu.
         *
         * autor:
         * Eryk Sikora
         *************************************************************************/
        private void EditPlayer()
        {
            if (!int.TryParse(PlayerIdToEdit, out int playerId)
                || playerId <= 0)
            {
                EditResult = "Podaj poprawne ID gracza";
                return;
            }

            if (string.IsNullOrWhiteSpace(PlayerNameToEdit))
            {
                EditResult = "Podaj nową nazwę gracza";
                return;
            }

            if (!int.TryParse(PlayerLevelToEdit, out int playerLevel)
                || playerLevel < 0)
            {
                EditResult = "Podaj poprawny, nieujemny poziom gracza";
                return;
            }

            EditPlayerCommand command = new EditPlayerCommand(
                playerId,
                PlayerNameToEdit.Trim(),
                playerLevel);

            bool edited = _editPlayerCommandHandler.Handle(command);
            if (!edited)
            {
                EditResult = "Nie znaleziono gracza";
                return;
            }

            LoadPlayers();
            EditResult = "Dane gracza zostały zaktualizowane";
            PlayerIdToEdit = "";
            PlayerNameToEdit = "";
            PlayerLevelToEdit = "";
            OnPropertyChanged(nameof(PlayerIdToEdit));
            OnPropertyChanged(nameof(PlayerNameToEdit));
            OnPropertyChanged(nameof(PlayerLevelToEdit));
        }

        /*************************************************************************
         * nazwa funkcji:
         * LoadPlayers
         *
         * parametry wejściowe:
         * brak
         *
         * wartość zwracana:
         * brak
         *
         * opis funkcji:
         * Pobiera graczy przez handler zapytania i odświeża kolekcję widoku.
         *
         * autor:
         * Eryk Sikora
         *************************************************************************/
        private void LoadPlayers()
        {
            GetPlayersQuery query = new GetPlayersQuery();

            List<Player> players =
                _getPlayersQueryHandler.Handle(query);

            Players.Clear();

            foreach (Player player in players)
            {
                Players.Add(player);
            }
        }

        /*************************************************************************
         * nazwa funkcji:
         * OnPropertyChanged
         *
         * parametry wejściowe:
         * propertyName, string, nazwa właściwości, której wartość się zmieniła
         *
         * wartość zwracana:
         * brak
         *
         * opis funkcji:
         * Powiadamia widok o zmianie wartości właściwości ViewModelu.
         *
         * autor:
         * Eryk Sikora
         *************************************************************************/
        protected void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}

namespace MauiApp3.Commands
{
    /*************************************************************************
     * nazwa klasy:
     * EditPlayerCommand
     *
     * opis:
     * Klasa reprezentuje polecenie zmiany danych gracza.
     * Przechowuje identyfikator gracza oraz jego nową nazwę i poziom.
     *
     * pola / właściwości:
     * Id, int, identyfikator gracza do edycji
     * Name, string, nowa nazwa gracza
     * Level, int, nowy poziom gracza
     *
     * autor:
     * Eryk Sikora
     *************************************************************************/
    public class EditPlayerCommand
    {
        public int Id { get; }
        public string Name { get; }
        public int Level { get; }

        /*************************************************************************
         * nazwa funkcji:
         * EditPlayerCommand
         *
         * parametry wejściowe:
         * id, int, identyfikator gracza do edycji
         * name, string, nowa nazwa gracza
         * level, int, nowy poziom gracza
         *
         * wartość zwracana:
         * brak
         *
         * opis funkcji:
         * Tworzy polecenie zmiany nazwy i poziomu wskazanego gracza.
         *
         * autor:
         * Eryk Sikora
         *************************************************************************/
        public EditPlayerCommand(int id, string name, int level)
        {
            Id = id;
            Name = name;
            Level = level;
        }
    }
}
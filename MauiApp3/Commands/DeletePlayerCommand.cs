namespace MauiApp3.Commands
{
    /*************************************************************************
     * nazwa klasy:
     * DeletePlayerCommand
     *
     * opis:
     * Klasa reprezentuje polecenie usunięcia gracza.
     * Przechowuje identyfikator gracza przeznaczonego do usunięcia.
     *
     * pola / właściwości:
     * Id, int, identyfikator gracza do usunięcia
     *
     * autor:
     * Eryk Sikora
     *************************************************************************/
    public class DeletePlayerCommand
    {
        public int Id { get; }

        /*************************************************************************
         * nazwa funkcji:
         * DeletePlayerCommand
         *
         * parametry wejściowe:
         * id, int, identyfikator gracza do usunięcia
         *
         * wartość zwracana:
         * brak
         *
         * opis funkcji:
         * Tworzy polecenie usunięcia gracza o podanym identyfikatorze.
         *
         * autor:
         * Eryk Sikora
         *************************************************************************/
        public DeletePlayerCommand(int id)
        {
            Id = id;
        }
    }
}


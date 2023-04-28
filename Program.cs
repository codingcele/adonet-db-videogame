
using System;   //il namespace System contiene le classi fondamentali e le funzionalità del framework .NET

using System.Data.SqlClient;   //il namespace System.Data.SqlClient contiene le classi che permettono di connettersi e comunicare con un database Microsoft SQL Server

namespace adonet_db_videogame
{
    public class Program
    {
        static void Main(string[] args)
        {

            ConsoleInteractions.Digit();

            string azione = Console.ReadLine();

            while (azione != "e")
            {
                switch (azione)
                {
                    case "i":
                        Console.WriteLine("Inserisci i dati del videogioco che vuoi inserire:");
                        Console.Write("Nome: ");
                        string name = Console.ReadLine();

                        Console.Write("Overview: ");
                        string overview = Console.ReadLine();

                        Console.Write("Release date (YYYY/MM/DD): ");
                        string date = Console.ReadLine();
                        DateTime releaseDate;
                        DateTime.TryParse(date, out releaseDate);

                        Console.Write("Software house id: ");
                        int softwareHouseId;
                        bool inputValido = int.TryParse(Console.ReadLine(), out softwareHouseId);

                        Videogame newVideogame = new Videogame(name, overview, releaseDate, softwareHouseId);

                        DateTime minDate = new DateTime(1753, 1, 1); 
                        DateTime maxDate = new DateTime(9999, 1, 1);

                        if (!(string.IsNullOrEmpty(name)) && (releaseDate <= DateTime.Now) && (releaseDate > minDate) && (releaseDate < maxDate) && (softwareHouseId >= 1) && (softwareHouseId <= 6))
                        {
                            VideogameManager.InsertVideogame(newVideogame);
                            Console.WriteLine("Videogame creato con successo!");
                        }

                        ConsoleInteractions.Digit();

                        azione = Console.ReadLine();

                        break;

                    case "sid":
                        Console.Write("Id videogioco: ");
                        int videogameId = int.Parse(Console.ReadLine());
                        VideogameManager.SearchById(videogameId);

                        ConsoleInteractions.Digit();

                        azione = Console.ReadLine();

                        break;

                    case "str":
                        Console.Write("Cerca videogioco: ");
                        string stringa = Console.ReadLine();
                        VideogameManager.SearchByName(stringa);

                        ConsoleInteractions.Digit();

                        azione = Console.ReadLine();

                        break;

                    case "d":
                        Console.Write("Id videogioco: ");
                        int id = int.Parse(Console.ReadLine());
                        VideogameManager.DeleteById(id);

                        ConsoleInteractions.Digit();

                        azione = Console.ReadLine();

                        break;

                    case "exit":
                        VideogameManager.CloseApp();

                        break;

                    default:
                        azione = Console.ReadLine();
                        break;
                }
            }

        }
    }
}
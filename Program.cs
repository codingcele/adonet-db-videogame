
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
                        string releaseDate = Console.ReadLine();

                        Console.Write("Software house id: ");
                        int softwareHouseId = int.Parse(Console.ReadLine());

                        VideogameManager.insertVideogame(name, overview, releaseDate, softwareHouseId);

                        ConsoleInteractions.Digit();

                        azione = Console.ReadLine();

                        break;

                    default:
                        azione = Console.ReadLine();
                        break;
                }
            }

        }
    }
}
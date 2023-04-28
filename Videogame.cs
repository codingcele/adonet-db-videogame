using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db_videogame
{
    internal class Videogame
    {
        public string Name { get; set; }
        public string Overview { get; set; }
        public DateTime Release_date { get; set; }
        public long Software_house_id { get; set; }

        public Videogame(string name, string overview, DateTime release_date, long software_house_id)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentException("Il nome del gioco non può essere nullo o vuoto.");
                }

                if (release_date > DateTime.Now)
                {
                    throw new ArgumentException("La data di rilascio del gioco non può essere nel futuro.");
                }

                DateTime minDate = new DateTime(1753, 1, 1);
                if (release_date < minDate)
                {
                    throw new ArgumentException("Data di rilascio non valida.");
                }

                if (software_house_id < 1 || software_house_id > 6)
                {
                    throw new ArgumentException("L'id della casa produttrice deve essere tra quelli disponibili.");
                }

                Name = name;
                Overview = overview;
                Release_date = release_date;
                Software_house_id = software_house_id;
            }

            catch (Exception ex)
            {
                DateTime minDate = new DateTime(1753, 1, 1);
                if (string.IsNullOrEmpty(name)) 
                {
                    Console.WriteLine("Il nome del gioco non può essere nullo o vuoto.");
                }
                else if (release_date > DateTime.Now)
                {
                    Console.WriteLine("La data di rilascio del gioco non può essere nel futuro.");
                }
                else if (software_house_id < 1 || software_house_id > 6)
                {
                    Console.WriteLine("L'id della casa produttrice deve essere tra quelli disponibili.");
                }
                else if (release_date < minDate)
                {
                    Console.WriteLine("La data di rilascio non è valida.");
                }
            }
           
        }
       
    }
}
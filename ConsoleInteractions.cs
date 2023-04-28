using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db_videogame
{
    internal class ConsoleInteractions
    {
        public static void Digit()
        {
            Console.WriteLine("Digita: ");
            Console.WriteLine("i per inserire un nuovo videogame.");
            Console.WriteLine("sid per cercare un videogame per id.");
            Console.WriteLine("str per cercare i videogames che contengono una stringa.");
            Console.WriteLine("d per eliminare un videogame per id.");
            Console.WriteLine("exit per chiudere il programma.");
        } 
    }
}

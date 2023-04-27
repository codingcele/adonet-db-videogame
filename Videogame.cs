using System;
using System.Collections.Generic;
using System.Linq;
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
            Name = name;
            Overview = overview;
            Release_date = release_date;
            Software_house_id = software_house_id;
        }
    }
}
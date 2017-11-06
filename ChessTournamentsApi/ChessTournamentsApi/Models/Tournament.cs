using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessTournamentsApi.Models
{
    public class Tournament
    {
        public Tournament()
        {
        }

        public Tournament(string name, bool hasEnded, string country)
        {
            Name = name;
            this.hasEnded = hasEnded;
            Country = country;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool hasEnded { get; set; }
        public string Country { get; set; }
    }
}

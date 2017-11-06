using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessTournamentsApi.Models
{
    public class Player
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string Category { get; set; }

        public Player(string Name, int Rating, string Category)
        {
            this.Name = Name;
            this.Category = Category;
            this.Rating = Rating;
        }
        public Player() { }
    }
}

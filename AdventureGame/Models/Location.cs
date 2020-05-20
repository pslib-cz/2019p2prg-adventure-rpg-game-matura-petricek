using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureGame.Models
{
    public class Location : ILocation
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int HP { get; set; }
        public double Level { get; set; }
        public int Money { get; set; }
    }
}

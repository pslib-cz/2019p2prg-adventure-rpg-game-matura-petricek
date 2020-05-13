using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureGame.Models
{
    public class GameState
    {
        public int MaxHp { get; set; }
        public int HP { get; set; }
        public Room Location { get; set; }
        public int Money { get; set; }
        public List<string> Equipment { get; set; }
        public double Level { get; set; }

    }
}

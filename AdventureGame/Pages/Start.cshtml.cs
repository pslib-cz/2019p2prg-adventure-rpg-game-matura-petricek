using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureGame.Models;
using AdventureGame.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdventureGame
{
    public class StartModel : PageModel
    {
        private GameService _gs;
        public Location Location { get; set; }
        public List<Connection> Targets { get; set; }
        public GameState State { get; set; }

        public StartModel(GameService gs)
        {
            _gs = gs;
        }

        public void OnGet()
        {
            _gs.Start();
            Location = _gs.Location;
            Targets = _gs.Targets;
            State = _gs.State;
        }
    }
}
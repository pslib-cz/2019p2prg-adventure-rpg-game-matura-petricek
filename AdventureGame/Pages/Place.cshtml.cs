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
    public class PlaceModel : PageModel
    {
        private GameService _gs;

        public PlaceModel(GameService gs)
        {
            _gs = gs;
        }

        public Location Location { get; set; }
        public List<Connection> Targets { get; set; }
        public GameState State { get; set; }
        public string Win { get; set; }
        public void OnGet(Room id)
        {
            _gs.FetchData();
            //
            _gs.RoomAction(id);
            _gs.State.Location = id;
            _gs.Store();
            Location = _gs.Location;
            Targets = _gs.Targets;
            State = _gs.State;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureGame.Models;
using AdventureGame.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdventureGame.Pages
{
    public class ShopModel : PageModel
    {
        private GameService _gs;
        [BindProperty]
        public GameState State { get; set; }

        public ShopModel(GameService gs)
        {
            _gs = gs;
        }


        public void OnGet()
        {
            _gs.FetchData();
            _gs.Store();
            State = _gs.State;
        }
        public void OnPost()
        {
            _gs.FetchData();
            _gs.State.Money -= 2;
            _gs.State.Equipment += 1;
            _gs.State.HP += 1;
            _gs.Store();
            State = _gs.State;
        }
    }
}
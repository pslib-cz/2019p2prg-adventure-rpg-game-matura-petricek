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
    public class RPSModel : PageModel
    {
        [BindProperty]
        public Choice Player { get; set; }
        public Choice Computer { get; set; }
        [BindProperty]
        public int Round { get; set; }
        [BindProperty]
        public int Wins { get; set; }
        private Random Random;
        public RPSModel(GameService gs)
        {
            Random = new Random();
            _gs = gs;
        }
        private GameService _gs;
        [BindProperty]
        public GameState State { get; set; }
        public string Message { get; set; }
        public bool Finished { get; set; }
        public void OnGet()
        {
            Player = Choice.None;
            Computer = Choice.None;
            Round = 0;
            Wins = 0;
            _gs.FetchData();
            State = _gs.State;
        }
        public void OnPost()
        {
            _gs.FetchData();
            Computer = (Choice)Random.Next(1, 4);
            if (Player == Choice.Rock && Computer == Choice.Scissors ||
                Player == Choice.Scissors && Computer == Choice.Paper ||
                Player == Choice.Paper && Computer == Choice.Rock)
            {
                _gs.State.Money += 5;
                _gs.State.Level += 1;
                Message = "You have won and received 5 Golds";
            }
            else if (Player == Computer)
            {
                Message = "You have drawn and received absolutely nothing";
            }
            else
            {
                Message = "You have lost the game...and your money";
                _gs.State.Money -= 5;
            }
            _gs.Store();
            
            Finished = true;
        }
    }
    public enum Choice
    {
        None,Rock,Scissors,Paper
    }
}
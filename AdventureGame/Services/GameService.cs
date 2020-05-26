using AdventureGame.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace AdventureGame.Services
{
    public class GameService
    {
        private readonly ISessionStorage<GameState> _ss;
        private readonly ILocationProvider _lp;
        private const string KEY = "AMAZINGADVENTURE";
        private const Room START_ROOM = Room.Start;
        public GameState State { get; private set; }
        public Location Location { get { return _lp.GetLocation(State.Location); } }
        public List<Connection> Targets { get { return _lp.GetConnectionsFrom(State.Location); } }
        public GameService(ISessionStorage<GameState> ss, ILocationProvider lp)
        {
            _ss = ss;
            _lp = lp;
            State = new GameState();
        }

        public void Start()
        {
            State = new GameState { MaxHp = 20, Location = START_ROOM, HP = 10, Level = 1, Money = 5, Equipment = 0, HasALoan = false };
            Store();
        }

        public void FetchData()
        {
            State = _ss.LoadOrCreate(KEY);
        }

        public void Store()
        {
            _ss.Save(KEY, State);
        }
        public void RoomAction(Room room)
        {
            State.HP += _lp.GetLocation(room).HP;
            State.Level += _lp.GetLocation(room).Level;
            State.Money += _lp.GetLocation(room).Money;

            switch (room)
            {
                case Room.Bank:
                    if (State.Money <= 0 && State.HasALoan == false)
                    {
                        State.Money += 5;
                        State.HasALoan = true;
                    }
                    if (State.HasALoan == true) _lp.GetLocation(room).Description = "You already took money from the bank!";
                    break;
                case Room.Home:
                    if (State.HP < 8)
                    {
                        State.HP += 2;
                    }
                    break;
                case Room.GameOver:
                    State.HP = 0;
                    break;
                case Room.Footprints:
                    State.HasAKey = true;
                    break;
            }
            if (State.HP >= State.MaxHp)
            {
                State.HP = State.MaxHp;
            }
            if (State.Money < -5)
            {
                State.Money = -5;
            }
            if (State.Level >= 15)
            {
                State.Level = 15;
            }
            Store();
        }
    }
}

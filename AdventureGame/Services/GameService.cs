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
        private List<string> Equipment { get; set; }
        private readonly Random random = new Random();
        public GameService(ISessionStorage<GameState> ss, ILocationProvider lp, Random random)
        {
            this.random = random;
            _ss = ss;
            _lp = lp;
            State = new GameState();
            Equipment = new List<string> { "boots", "leggings", "helmet" };
        }

        public void Start()
        {
            State = new GameState { MaxHp = 10, Location = START_ROOM, HP = 10, Level = 1, Money = 5, Equipment= 0,};
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
                    if (State.Money <= 0)
                    {
                        State.Money += 5;
                    }
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
                /*case Room.Shop:
                    int item = random.Next(Equipment.Count);
                    if (!State.Equipment.Contains(Equipment[item]))
                    {
                        State.Equipment.Add(Equipment[item]);
                    }
                    else
                    {
                        RoomAction(room);
                    }
                    break;*/
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

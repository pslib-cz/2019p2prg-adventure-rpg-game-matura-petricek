using AdventureGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public GameService(ISessionStorage<GameState> ss, ILocationProvider lp)
        {
            _ss = ss;
            _lp = lp;
            State = new GameState();
            Equipment = new List<string> { "boots", "leggings", "helmet" };
        }

        public void Start()
        {
            State = new GameState { MaxHp = 10, Location = START_ROOM, HP = 10, Level = 1, Money = 0};
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
            if (State.HP <= 0)
            {
                State.Location = Room.GameOver;
            }
            else
            {
                switch (room)
                {
                    case Room.Pathway:
                        State.Level += 0.5;
                        State.Money += 2;
                        break;
                    case Room.FakeHome:
                        State.Level += 0.5;
                        break;
                    case Room.WaspsA:
                        State.HP -= 2;
                        State.Level += 0.5;
                        break;
                    case Room.WaspsK:
                        State.HP -= 1;
                        State.Level += 0.5;
                        break;
                    case Room.Hall:
                        State.Level += 1;
                        break;
                    case Room.Bank:
                        if (State.Money < 0)
                        {
                            State.Money += 5;
                        }
                        break;
                    case Room.Cave:
                        State.Money += 1;
                        State.Level += 1;
                        break;
                    case Room.Home:
                        if(State.HP < 8)
                        {
                            State.HP += 2;
                        }
                        break;
                    case Room.GameRoom:
                        break;
                    case Room.Library:
                        State.Money -= 2;
                        State.Level += 2;
                        break;
                    case Room.Fight:
                        State.Level += 2;
                        State.HP -= 5;
                        break;
                    case Room.GameOver:
                        State.Level = 0;
                        State.HP = 0;
                        State.Money = 0;
                        break;
                    /*case Room.Shop:
                        var rnd = new Random();
                        int random = rnd.Next(Equipment.Count);
                        if (!State.Equipment.Contains(Equipment[random]))
                        {
                            State.Equipment.Add(Equipment[random]);
                        }
                        else
                        {
                            RoomAction(room);
                        }
                        State.Money -= 20;
                        break;*/
                }
                if(State.Money < -5)
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
}

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
            if(State.HP <= 0)
            {
                State.Location = Room.GameOver;
            }
            if (State.Level == 10)
            {
                State.Location = Room.WinRoom;   
            }
            else
            {
                switch (room)
                {
                    case Room.Cave:
                        State.HP -= 3;
                        State.Level += 0.5;
                        break;
                    case Room.Home:
                        if(State.HP < 10)
                        {
                            State.HP += 2;
                        }
                        break;
                    case Room.Bank:
                        if(State.Money < 0)
                        {
                            State.Money = 5;
                        }
                        break;
                    case Room.GameRoom:

                        //State.Money -= 5;
                        //State.Level += 0.2;
                        break;
                    case Room.Farm:
                        State.Level += 0.3;
                        State.Money += 5;
                        break;
                    case Room.Library:
                        State.Money -= 2;
                        State.Level += 2;
                        break;
                    case Room.Shop:
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
                        break;
                }
                if(State.Money < -5)
                {
                    State.Money = -5;
                }
                Store();
            }


        }
    }
}

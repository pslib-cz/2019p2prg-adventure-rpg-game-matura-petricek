using Adventure2020.Models.Exceptions;
using AdventureGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureGame.Services
{
    public class LocationProvider : ILocationProvider
    {
        private Dictionary<Room, ILocation> _locations;
        private List<Connection> _map;

        public LocationProvider()
        {
            _locations = new Dictionary<Room, ILocation>();
            _map = new List<Connection>();
            _locations.Add(Room.Start, new Location { Title = "Old ruined house", Description = "You woke up in an old ruined house. You don't remember anything. " +
            "Behind some rusty doors you see a hidden pathway." }); // Game starts
            _locations.Add(Room.Pathway, new Location { Title = "Pathway", Level = 0.5, Money = 2,Description = "This path was really long, but it looks like it brought me to some hall." });
            _locations.Add(Room.FakeHome, new Location { Title = "Home", Level = 0.5, Description = "On your way home you came across some aggressive wasps. What do you do?" });
            _locations.Add(Room.WaspsA, new Location { Title = "Wasps", HP = -2, Level = 0.5, Description = "You attacked and so did the wasps." });
            _locations.Add(Room.WaspsK, new Location { Title = "Wasps", HP = -1, Level = 0.5, Description = "You were calm...but the wasps were not." });
            _locations.Add(Room.Hall, new Location { Title = "Hall", Level = 1, Description = "You stand in seemingly empty hall...and then you see a familiar thing...It's a way to your home!" });
            _locations.Add(Room.Bank, new Location { Title = "Bank", Description = "A bank with no interest rate? Seems shady..." });
            _locations.Add(Room.Cave, new Location { Title = "Cave", Level = 1, Money = 1, Description = "Strange sounds were coming from here." });
            _locations.Add(Room.Home, new Location { Title = "Home", Description = "Home sweet home." });
            _locations.Add(Room.GameRoom, new Location { Title = "Game Room", Description = "Welcome to the casino." });
            _locations.Add(Room.Library, new Location { Title = "Library", Level = 2, Money = -2, Description = "You followed the light and entered the library. You see a strange creature looking at you..." });
            _locations.Add(Room.Fight, new Location { Title = "Big fight", HP = -5, Level = 2 });
            _locations.Add(Room.GameOver, new Location { Title = "Game Over", Description = "You just died...and all the memories with you." }); // Game Over
            _locations.Add(Room.WinRoom, new Location { Title = "The End", Description = "You Finished the game!" });

            _map.Add(new Connection(Room.Start, Room.Pathway, "Explore the pathway"));
            _map.Add(new Connection(Room.Start, Room.FakeHome, "Try to go home"));
            _map.Add(new Connection(Room.Pathway, Room.Hall, "Enter the hall"));
            _map.Add(new Connection(Room.FakeHome, Room.WaspsA, "Attack them"));
            _map.Add(new Connection(Room.FakeHome, Room.WaspsK, "Keep calm"));
            _map.Add(new Connection(Room.WaspsA, Room.Start, "Fall back"));
            _map.Add(new Connection(Room.WaspsK, Room.Start, "Fall back"));
            _map.Add(new Connection(Room.Hall, Room.Bank, "Go to the bank"));
            _map.Add(new Connection(Room.Bank, Room.Hall, "Go back to the Hall"));
            _map.Add(new Connection(Room.Hall, Room.Home, "Go home"));
            _map.Add(new Connection(Room.Home, Room.Hall, "Go to the hall"));
            _map.Add(new Connection(Room.Home, Room.GameRoom, "Go Gambling"));
            _map.Add(new Connection(Room.Hall, Room.Cave, "Go to a cave"));
            _map.Add(new Connection(Room.Cave, Room.GameOver, "Follow the sounds"));
            _map.Add(new Connection(Room.Cave, Room.Library, "Follow the light"));
            _map.Add(new Connection(Room.Library, Room.GameOver, "Shout at it"));
            _map.Add(new Connection(Room.Library, Room.Fight, "Fight it"));
            _map.Add(new Connection(Room.Fight, Room.WinRoom, "Continue"));
        }

        public bool ExistsLocation(Room id)
        {
            return _locations.ContainsKey(id);
        }

        public List<Connection> GetConnectionsFrom(Room id)
        {
            if (ExistsLocation(id))
            {
                return _map.Where(m => m.From == id).ToList();
            }
            throw new InvalidLocation();
        }

        public List<Connection> GetConnectionsTo(Room id)
        {
            throw new NotImplementedException();
        }

        public Location GetLocation(Room id)
        {
            if (ExistsLocation(id))
            {
                return (Location)_locations[id];
            }
            throw new InvalidLocation();
        }

        public bool IsNavigationLegitimate(Room from, Room to, GameState state)
        {
            throw new NotImplementedException();
        }
    }
}

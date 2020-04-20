using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureGame.Models;

namespace AdventureGame.Services
{
    public class Location
    {
        public Dictionary<int, Room> _locations;

        public Location()
        {
            _locations = new Dictionary<int, Room>();
            _list = new List<Room>();
            _locations.Add(0, new Room { Description = "This is where our story starts.", RoomName ="1"}  ); // Game starts
            _locations.Add(1, new Room { Description = "All worldly things will one day perish. You just did.", RoomName = "2" }  ); // Game Over
            _locations.Add(2, new Room { Description = "You stand in seemingly empty hall ...", RoomName = "3" }  );
            _locations.Add(3, new Room { Description = "Library is in utterly desolate state ...", RoomName = "4" } );
        }
        public void GetLocation(int value)
        {
            // _locations[value];
        }
    
    }
}

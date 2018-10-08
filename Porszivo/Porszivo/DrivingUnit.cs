using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Porszivo
{
    public class DrivingUnit
    {
        private Room Room { get; }        

        public DrivingUnit(Room room)
        {
            Room = room;
        }

        public void Move(Direction direction)
        {
            Room.moveRobot(direction);
        }
    }
}
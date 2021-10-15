using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Location
{
    class Map
    { 
        Room CurrentPosition { get; set; }



        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.NORTH:
                    CurrentPosition = CurrentPosition.North;
                    break;
                case Direction.SOUTH:
                    CurrentPosition = CurrentPosition.South;
                    break;
                case Direction.EAST:
                    CurrentPosition = CurrentPosition.East;
                    break;
                case Direction.WEST:
                    CurrentPosition = CurrentPosition.West;
                    break;
            }
        }
    }
}

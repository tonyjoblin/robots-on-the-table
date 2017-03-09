using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robot
{
    class Direction
    {
        public enum DirectionName { NORTH, WEST, SOUTH, EAST };

        public static DirectionName TurnLeft(DirectionName facing)
        {
            if (facing == DirectionName.EAST)
            {
                return DirectionName.NORTH;
            }
            return ++facing;
        }

        public static DirectionName TurnRight(DirectionName facing)
        {
            if (facing == DirectionName.NORTH)
            {
                return DirectionName.EAST;
            }
            return --facing;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robot
{
    class Direction
    {
        public enum DirectionName { UP, LEFT, DOWN, RIGHT };

        public static DirectionName TurnLeft(DirectionName facing)
        {
            if (facing == DirectionName.RIGHT)
            {
                return DirectionName.UP;
            }
            return ++facing;
        }

        public static DirectionName TurnRight(DirectionName facing)
        {
            if (facing == DirectionName.UP)
            {
                return DirectionName.RIGHT;
            }
            return --facing;
        }
    }
}

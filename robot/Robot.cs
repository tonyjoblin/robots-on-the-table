using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robot
{
    class Robot
    {
        public Robot()
        {
            Placed = false;
        }

        public Robot(Robot initialState)
        {
            X = initialState.X;
            Y = initialState.Y;
            Facing = initialState.Facing;
            Placed = initialState.Placed;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        

        public enum Direction { UP, LEFT, DOWN, RIGHT };
        public Direction Facing { get; private set; }

        public bool Placed { get; private set; }

        public void Place(int x, int y, Direction facing)
        {
            X = x;
            Y = y;
            Facing = facing;
            Placed = true;
        }

        public void Move()
        {
            switch (Facing)
            {
                case Direction.UP:
                    Y++;
                    break;
                case Direction.DOWN:
                    Y--;
                    break;
                case Direction.LEFT:
                    X--;
                    break;
                case Direction.RIGHT:
                    X++;
                    break;
                default:
                    break;
            };
        }
    }
}

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
        
        public Direction.DirectionName Facing { get; private set; }

        public bool Placed { get; private set; }

        public void Place(int x, int y, Direction.DirectionName facing)
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
                case Direction.DirectionName.UP:
                    Y++;
                    break;
                case Direction.DirectionName.DOWN:
                    Y--;
                    break;
                case Direction.DirectionName.LEFT:
                    X--;
                    break;
                case Direction.DirectionName.RIGHT:
                    X++;
                    break;
                default:
                    break;
            };
        }

        public void TurnLeft()
        {
            Facing = Direction.TurnLeft(Facing);
        }

        public void TurnRight()
        {
            Facing = Direction.TurnRight(Facing);
        }
    }
}

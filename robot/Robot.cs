using System;

namespace robot
{
    class Robot: IEquatable<Robot>
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

        public Robot(bool placed, int x, int y, Direction.DirectionName facing)
        {
            Placed = placed;
            X = x;
            Y = y;
            Facing = facing;
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
                case Direction.DirectionName.NORTH:
                    Y++;
                    break;
                case Direction.DirectionName.SOUTH:
                    Y--;
                    break;
                case Direction.DirectionName.WEST:
                    X--;
                    break;
                case Direction.DirectionName.EAST:
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

        public override string ToString()
        {
            if (!Placed)
            {
                return "In toy box";
            }
            return string.Format("{0},{1},{2}", X, Y, Facing);
        }

        public override bool Equals(object other)
        {
            return Equals(other as Robot);
        }

        public bool Equals(Robot other)
        {
            return ToString().CompareTo(other.ToString()) == 0;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}

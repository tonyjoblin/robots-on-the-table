using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robot
{
    class RobotController
    {
        public RobotController(Robot robot)
        {
            Robot = robot;
        }
        public void Run(System.IO.TextReader input, System.IO.TextWriter output)
        {
            if (input == null)
            {
                return;
            }
            do
            {
                var inputString = input.ReadLine();
                if (inputString == null)
                {
                    break;
                }

                var commandAndArgs = ParseCommand(inputString);
                var command = commandAndArgs.Item1;
                var args = commandAndArgs.Item2;

                if (command == "REPORT")
                {
                    output.WriteLine(Robot);
                }
                else if (!Robot.Placed && command == "PLACE")
                {
                    HandlePlaceCmd(args);
                }

            }
            while (true);
        }

        private void HandlePlaceCmd(string args)
        {
            if (args == null)
            {
                return;
            }
            var parts = args.Split(',');
            if (parts.Length != 3)
            {
                return;
            }

            int x;
            if (!int.TryParse(parts[0], out x))
            {
                return;
            }

            int y;
            if (!int.TryParse(parts[1], out y))
            {
                return;
            }

            Direction.DirectionName dir;
            if (!Direction.DirectionName.TryParse(parts[2].ToUpper(), false, out dir))
            {
                return;
            }

            var newState = new Robot(Robot);
            newState.Place(x, y, dir);
            if (FallsOff(newState))
            {
                return;
            }
            Robot = newState;
        }

        private bool FallsOff(Robot newState)
        {
            return newState.X < 0 || newState.Y < 0 || newState.X > 4 || newState.Y > 4;
        }

        public Robot Robot { get; private set; }

        private Tuple<string, string> ParseCommand(string input)
        {
            char[] ws = new char[] { ' ' };
            input = input.TrimStart(ws);

            int pos = input.IndexOf(' ');
            if (pos == -1)
            {
                return Tuple.Create(input.ToUpper(), null as string);
            }

            return Tuple.Create(input.Substring(0, pos).ToUpper(), input.Substring(pos + 1));
        }
    }
}

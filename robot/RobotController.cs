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

                if (command == "REPORT")
                {
                    output.WriteLine(Robot);
                }

            }
            while (true);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robot
{
    class RobotController
    {
        private Robot m_robot;

        public RobotController(Robot robot)
        {
            m_robot = robot;
        }
        public void Run(System.IO.TextReader input, System.IO.TextWriter output)
        {
            if (input == null)
            {
                return;
            }
            do
            {
                var command = input.ReadLine();
                if (command == null)
                {
                    break;
                }
            }
            while (true);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robot
{
    class Program
    {
        static void Main(string[] args)
        {
            var robot = new Robot();
            var controller = new RobotController(robot);
            controller.Run(Console.In, Console.Out);
        }
    }
}

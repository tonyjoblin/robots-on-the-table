using System;
using robot;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace robot_unittest
{
    [TestClass]
    public class RobotControllerUnitTests
    {
        [TestMethod]
        public void Run_NullInputStream_NoThrow()
        {
            var robot = new Robot();
            var controller = new RobotController(robot);
            controller.Run(null, null);
        }


    }
}

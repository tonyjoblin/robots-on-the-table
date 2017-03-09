using System;
using System.IO;
using robot;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace robot_unittest
{
    [TestClass]
    public class RobotControllerUnitTests
    {
        [TestMethod]
        public void Run_NullStreams_NoThrow()
        {
            var robot = new Robot();
            var controller = new RobotController(robot);
            controller.Run(null, null);
        }

        struct RobotResult
        {
            public RobotResult(Robot state, string output)
            {
                Output = output;
                FinalState = state;
            }
            public string  Output;
            public Robot   FinalState;
        }

        private RobotResult RunRobot(string commands, Robot initialState)
        {
            if (initialState == null)
            {
                initialState = new Robot();
            }
            using (var reader = new StringReader(commands))
            {
                using (var writer = new StringWriter())
                {
                    var robot = new Robot(initialState);
                    var controller = new RobotController(robot);

                    controller.Run(reader, writer);

                    var finalState = controller.Robot;

                    return new RobotResult(finalState, writer.ToString());
                }
            }
        }

        [TestMethod]
        public void Run_EmptyInput_DoesNothing()
        {
            var initialState = new Robot();

            var result = RunRobot("", initialState);

            Assert.AreEqual("", result.Output);
            Assert.AreEqual(initialState, result.FinalState);
        }

        [TestMethod]
        public void Run_PlaceOnTable_Ok()
        {
            var initialState = new Robot();

            var result = RunRobot("PLACE 1,1,UP", initialState);

            Assert.AreEqual("", result.Output);
            var expectedState = new Robot(true, 1, 1, Direction.DirectionName.UP);
            Assert.AreEqual(expectedState, result.FinalState);
        }

        [TestMethod]
        public void Run_PlaceOffTable_Ignored()
        {
            var initialState = new Robot();

            var result = RunRobot("PLACE -1,-1,UP", initialState);

            Assert.AreEqual("", result.Output);
            Assert.AreEqual(initialState, result.FinalState);
        }

        [TestMethod]
        public void Run_Report()
        {
            var initialState = new Robot();

            var result = RunRobot("REPORT", initialState);

            Assert.AreEqual("In toy box\r\n", result.Output);
        }

        [TestMethod]
        public void Run_WhiteSpaceBeforeCommand()
        {
            var initialState = new Robot();

            var result = RunRobot(" REPORT", initialState);

            Assert.AreEqual("In toy box\r\n", result.Output);
        }

        [TestMethod]
        public void Run_WhiteSpaceAfterCommand()
        {
            var initialState = new Robot();

            var result = RunRobot("REPORT ", initialState);

            Assert.AreEqual("In toy box\r\n", result.Output);
        }

        [TestMethod]
        public void Run_LowerCaseCommand()
        {
            var initialState = new Robot();

            var result = RunRobot("report", initialState);

            Assert.AreEqual("In toy box\r\n", result.Output);
        }
    }
}

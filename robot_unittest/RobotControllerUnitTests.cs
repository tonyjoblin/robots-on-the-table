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

            var result = RunRobot("PLACE 1,1,NORTH", initialState);

            Assert.AreEqual("", result.Output);
            var expectedState = new Robot(true, 1, 1, Direction.DirectionName.NORTH);
            Assert.AreEqual(expectedState, result.FinalState);
        }

        [TestMethod]
        public void Run_PlaceOffTable_Ignored()
        {
            var initialState = new Robot();

            var result = RunRobot("PLACE -1,-1,NORTH", initialState);

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
        public void Run_ReportOnTable()
        {
            var initialState = new Robot(true, 2, 2, Direction.DirectionName.WEST);

            var result = RunRobot("REPORT", initialState);

            Assert.AreEqual("2,2,WEST\r\n", result.Output);
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

        [TestMethod]
        public void Run_PlaceNoArgs_Ignore()
        {
            var initialState = new Robot();

            var result = RunRobot("place", initialState);

            Assert.AreEqual("", result.Output);
            Assert.AreEqual(initialState, result.FinalState);
        }

        [TestMethod]
        public void Run_PlaceOffTable_Ignore()
        {
            var initialState = new Robot();

            var result = RunRobot("place -1,2,NORTH", initialState);
            Assert.AreEqual("", result.Output);
            Assert.AreEqual(initialState, result.FinalState);

            result = RunRobot("place 2,-1,NORTH", initialState);
            Assert.AreEqual("", result.Output);
            Assert.AreEqual(initialState, result.FinalState);

            result = RunRobot("place 5,2,NORTH", initialState);
            Assert.AreEqual("", result.Output);
            Assert.AreEqual(initialState, result.FinalState);

            result = RunRobot("place 2,5,NORTH", initialState);
            Assert.AreEqual("", result.Output);
            Assert.AreEqual(initialState, result.FinalState);
        }

        [TestMethod]
        public void Run_PlaceWhiteSpaceAroundArgs_Ok()
        {
            var initialState = new Robot();

            var result = RunRobot("place  2 ,2 ,NORTH ", initialState);
            var expectedState = new Robot(true, 2, 2, Direction.DirectionName.NORTH);
            Assert.AreEqual("", result.Output);
            Assert.AreEqual(expectedState, result.FinalState);
        }

        [TestMethod]
        public void Run_PlaceMixedCaseDirectionArg_Ok()
        {
            var initialState = new Robot();
            var expectedState = new Robot(true, 2, 2, Direction.DirectionName.NORTH);

            var result = RunRobot("place 2,2,North ", initialState);
            Assert.AreEqual("", result.Output);
            Assert.AreEqual(expectedState, result.FinalState);

            result = RunRobot("place 2,2,north ", initialState);
            Assert.AreEqual("", result.Output);
            Assert.AreEqual(expectedState, result.FinalState);

            result = RunRobot("place 2,2,nORth ", initialState);
            Assert.AreEqual("", result.Output);
            Assert.AreEqual(expectedState, result.FinalState);
        }

        [TestMethod]
        public void Run_TurnLeftWhenNotPlaced_Ignored()
        {
            var initialState = new Robot();
            var expectedState = new Robot();

            var result = RunRobot("WEST", initialState);
            Assert.AreEqual("", result.Output);
            Assert.AreEqual(expectedState, result.FinalState);
        }

        [TestMethod]
        public void Run_TurnLeft_Rotates()
        {
            var initialState = new Robot(true, 2, 2, Direction.DirectionName.NORTH);
            var result = RunRobot("LEFT", initialState);
            var expectedState = new Robot(true, 2, 2, Direction.DirectionName.WEST);
            Assert.AreEqual(expectedState, result.FinalState);

            initialState = new Robot(true, 2, 2, Direction.DirectionName.NORTH);
            result = RunRobot("LEFT\r\nLEFT", initialState);
            expectedState = new Robot(true, 2, 2, Direction.DirectionName.SOUTH);
            Assert.AreEqual(expectedState, result.FinalState);

            initialState = new Robot(true, 2, 2, Direction.DirectionName.NORTH);
            result = RunRobot("LEFT\r\nLEFT\r\nLEFT", initialState);
            expectedState = new Robot(true, 2, 2, Direction.DirectionName.EAST);
            Assert.AreEqual(expectedState, result.FinalState);

            initialState = new Robot(true, 2, 2, Direction.DirectionName.NORTH);
            result = RunRobot("LEFT\r\nLEFT\r\nLEFT\r\nLEFT", initialState);
            expectedState = new Robot(true, 2, 2, Direction.DirectionName.NORTH);
            Assert.AreEqual(expectedState, result.FinalState);
        }

        [TestMethod]
        public void Run_TurnRightWhenNotPlaced_Ignored()
        {
            var initialState = new Robot();
            var expectedState = new Robot();

            var result = RunRobot("EAST", initialState);
            Assert.AreEqual("", result.Output);
            Assert.AreEqual(expectedState, result.FinalState);
        }

        [TestMethod]
        public void Run_TurnRight_Rotates()
        {
            var initialState = new Robot(true, 2, 2, Direction.DirectionName.NORTH);
            var result = RunRobot("RIGHT", initialState);
            var expectedState = new Robot(true, 2, 2, Direction.DirectionName.EAST);
            Assert.AreEqual(expectedState, result.FinalState);

            initialState = new Robot(true, 2, 2, Direction.DirectionName.NORTH);
            result = RunRobot("RIGHT\r\nRIGHT", initialState);
            expectedState = new Robot(true, 2, 2, Direction.DirectionName.SOUTH);
            Assert.AreEqual(expectedState, result.FinalState);

            initialState = new Robot(true, 2, 2, Direction.DirectionName.NORTH);
            result = RunRobot("RIGHT\r\nRIGHT\r\nRIGHT", initialState);
            expectedState = new Robot(true, 2, 2, Direction.DirectionName.WEST);
            Assert.AreEqual(expectedState, result.FinalState);

            initialState = new Robot(true, 2, 2, Direction.DirectionName.NORTH);
            result = RunRobot("RIGHT\r\nRIGHT\r\nRIGHT\r\nRIGHT", initialState);
            expectedState = new Robot(true, 2, 2, Direction.DirectionName.NORTH);
            Assert.AreEqual(expectedState, result.FinalState);
        }

        [TestMethod]
        public void Run_MoveNotPlaced_Ignored()
        {
            var initialState = new Robot();

            var result = RunRobot("MOVE", initialState);

            var expectedState = new Robot();
            Assert.AreEqual("", result.Output);
            Assert.AreEqual(expectedState, result.FinalState);
        }

        [TestMethod]
        public void Run_MoveOnTable_Ok()
        {
            var initialState = new Robot(true, 2, 2, Direction.DirectionName.NORTH);

            var result = RunRobot("MOVE", initialState);

            var expectedState = new Robot(true, 2, 3, Direction.DirectionName.NORTH);
            Assert.AreEqual("", result.Output);
            Assert.AreEqual(expectedState, result.FinalState);
        }

        [TestMethod]
        public void Run_MoveUpOffTable_Ignored()
        {
            var initialState = new Robot(true, 2, 4, Direction.DirectionName.NORTH);
            var expectedState = new Robot(initialState);

            var result = RunRobot("MOVE", initialState);

            Assert.AreEqual("", result.Output);
            Assert.AreEqual(expectedState, result.FinalState);
        }

        [TestMethod]
        public void Run_MoveLeftOffTable_Ignored()
        {
            var initialState = new Robot(true, 0, 2, Direction.DirectionName.WEST);
            var expectedState = new Robot(initialState);

            var result = RunRobot("MOVE", initialState);

            Assert.AreEqual("", result.Output);
            Assert.AreEqual(expectedState, result.FinalState);
        }

        [TestMethod]
        public void Run_MoveDownOffTable_Ignored()
        {
            var initialState = new Robot(true, 2, 0, Direction.DirectionName.SOUTH);
            var expectedState = new Robot(initialState);

            var result = RunRobot("MOVE", initialState);

            Assert.AreEqual("", result.Output);
            Assert.AreEqual(expectedState, result.FinalState);
        }

        [TestMethod]
        public void Run_MoveRightOffTable_Ignored()
        {
            var initialState = new Robot(true, 4, 2, Direction.DirectionName.EAST);
            var expectedState = new Robot(initialState);

            var result = RunRobot("MOVE", initialState);

            Assert.AreEqual("", result.Output);
            Assert.AreEqual(expectedState, result.FinalState);
        }

        [TestMethod]
        public void Run_DoublePlace_Ignored()
        {
            var initialState = new Robot(true, 2, 2, Direction.DirectionName.EAST);
            
            var result = RunRobot("PLACE 1,1,NORTH", initialState);

            var expectedState = new Robot(true, 1, 1, Direction.DirectionName.NORTH);
            Assert.AreEqual("", result.Output);
            Assert.AreEqual(expectedState, result.FinalState);
        }

        [TestMethod]
        public void Run_Example_A()
        {
            var initialState = new Robot();

            var result = RunRobot("PLACE 0,0,NORTH\r\nMOVE\r\nREPORT", initialState);

            var expectedState = new Robot(true, 0, 1, Direction.DirectionName.NORTH);
            Assert.AreEqual("0,1,NORTH\r\n", result.Output);
            Assert.AreEqual(expectedState, result.FinalState);
        }

        [TestMethod]
        public void Run_Example_B()
        {
            var initialState = new Robot();

            var result = RunRobot("PLACE 0,0,NORTH\r\nLEFT\r\nREPORT", initialState);

            var expectedState = new Robot(true, 0, 0, Direction.DirectionName.WEST);
            Assert.AreEqual("0,0,WEST\r\n", result.Output);
            Assert.AreEqual(expectedState, result.FinalState);
        }

        [TestMethod]
        public void Run_Example_C()
        {
            var initialState = new Robot();

            var result = RunRobot(
                "PLACE 1,2,EAST\r\nMOVE\r\nMOVE\r\nLEFT\r\nMOVE\r\nREPORT",
                initialState
                );

            var expectedState = new Robot(true, 3, 3, Direction.DirectionName.NORTH);
            Assert.AreEqual("3,3,NORTH\r\n", result.Output);
            Assert.AreEqual(expectedState, result.FinalState);
        }
    }
}

using System;
using robot;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace robot_unittest
{
    [TestClass]
    public class RobotUnitTest
    {
        [TestMethod]
        public void DefaultConstructor_InitialState_OffTable()
        {
            var r = new Robot();
            Assert.AreEqual(false, r.Placed);
        }

        [TestMethod]
        public void ConstructorWithInitialState_SetsState()
        {
            var r = new Robot(true, -1, 17, Direction.DirectionName.WEST);
            Assert.AreEqual(true, r.Placed);
            Assert.AreEqual(-1, r.X);
            Assert.AreEqual(17, r.Y);
            Assert.AreEqual(Direction.DirectionName.WEST, r.Facing);
        }

        [TestMethod]
        public void Place_SetsState()
        {
            int x = 3;
            int y = 7;
            var facing = Direction.DirectionName.SOUTH;
            var r = new Robot();

            r.Place(x, y, facing);

            Assert.AreEqual(true, r.Placed);
            Assert.AreEqual(x, r.X);
            Assert.AreEqual(y, r.Y);
            Assert.AreEqual(facing, r.Facing);
        }

        [TestMethod]
        public void CopyConstructor_CopiesState()
        {
            int x = 3;
            int y = 7;
            var facing = Direction.DirectionName.SOUTH;
            var initialState = new Robot();
            initialState.Place(x, y, facing);

            var nextState = new Robot(initialState);

            Assert.AreEqual(true, nextState.Placed);
            Assert.AreEqual(x, nextState.X);
            Assert.AreEqual(y, nextState.Y);
            Assert.AreEqual(facing, nextState.Facing);
        }

        [TestMethod]
        public void Move_Up()
        {
            var robot = new Robot();
            robot.Place(3, 7, Direction.DirectionName.NORTH);

            robot.Move();

            Assert.AreEqual(3, robot.X);
            Assert.AreEqual(8, robot.Y);
            Assert.AreEqual(Direction.DirectionName.NORTH, robot.Facing);
        }

        [TestMethod]
        public void Move_Down()
        {
            var robot = new Robot();
            robot.Place(3, 7, Direction.DirectionName.SOUTH);

            robot.Move();

            Assert.AreEqual(3, robot.X);
            Assert.AreEqual(6, robot.Y);
            Assert.AreEqual(Direction.DirectionName.SOUTH, robot.Facing);
        }

        [TestMethod]
        public void Move_Left()
        {
            var robot = new Robot();
            robot.Place(3, 7, Direction.DirectionName.WEST);

            robot.Move();

            Assert.AreEqual(2, robot.X);
            Assert.AreEqual(7, robot.Y);
            Assert.AreEqual(Direction.DirectionName.WEST, robot.Facing);
        }

        [TestMethod]
        public void Move_Right()
        {
            var robot = new Robot();
            robot.Place(3, 7, Direction.DirectionName.EAST);

            robot.Move();

            Assert.AreEqual(4, robot.X);
            Assert.AreEqual(7, robot.Y);
            Assert.AreEqual(Direction.DirectionName.EAST, robot.Facing);
        }

        [TestMethod]
        public void ToString_NotPlaced()
        {
            var robot = new Robot();

            var toString = robot.ToString();

            Assert.AreEqual("In toy box", toString);
        }

        [TestMethod]
        public void ToString_Placed_SomeWhere()
        {
            var robot = new Robot();
            robot.Place(3, 7, Direction.DirectionName.SOUTH);

            var toString = robot.ToString();

            Assert.AreEqual(0, toString.CompareTo("3,7,SOUTH"));
        }

        [TestMethod]
        public void Equals()
        {
            var robot = new Robot();
            robot.Place(3, 7, Direction.DirectionName.SOUTH);
            var inToyBox = new Robot();
            var somewhereElse = new Robot();
            robot.Place(2, 4, Direction.DirectionName.WEST);

            Assert.AreEqual(true, robot.Equals(robot));
            Assert.AreEqual(false, robot.Equals(somewhereElse));
            Assert.AreEqual(false, robot.Equals(inToyBox));
            Assert.AreEqual(false, somewhereElse.Equals(robot));
            Assert.AreEqual(false, inToyBox.Equals(robot));
        }
    }
}

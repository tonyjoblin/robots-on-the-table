﻿using System;
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
        public void Place_SetsState()
        {
            int x = 3;
            int y = 7;
            var facing = Robot.Direction.DOWN;
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
            var facing = Robot.Direction.DOWN;
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
            robot.Place(3, 7, Robot.Direction.UP);

            robot.Move();

            Assert.AreEqual(3, robot.X);
            Assert.AreEqual(8, robot.Y);
            Assert.AreEqual(Robot.Direction.UP, robot.Facing);
        }

        [TestMethod]
        public void Move_Down()
        {
            var robot = new Robot();
            robot.Place(3, 7, Robot.Direction.DOWN);

            robot.Move();

            Assert.AreEqual(3, robot.X);
            Assert.AreEqual(6, robot.Y);
            Assert.AreEqual(Robot.Direction.DOWN, robot.Facing);
        }

        [TestMethod]
        public void Move_Left()
        {
            var robot = new Robot();
            robot.Place(3, 7, Robot.Direction.LEFT);

            robot.Move();

            Assert.AreEqual(2, robot.X);
            Assert.AreEqual(7, robot.Y);
            Assert.AreEqual(Robot.Direction.LEFT, robot.Facing);
        }

        [TestMethod]
        public void Move_Right()
        {
            var robot = new Robot();
            robot.Place(3, 7, Robot.Direction.RIGHT);

            robot.Move();

            Assert.AreEqual(4, robot.X);
            Assert.AreEqual(7, robot.Y);
            Assert.AreEqual(Robot.Direction.RIGHT, robot.Facing);
        }
    }
}

using System;
using robot;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace robot_unittest
{
    [TestClass]
    public class DirectionUnitTest
    {
        [TestMethod]
        public void Left()
        {
            var d = Direction.DirectionName.WEST;

            d = Direction.TurnLeft(d);
            Assert.AreEqual(Direction.DirectionName.SOUTH, d);

            d = Direction.TurnLeft(d);
            Assert.AreEqual(Direction.DirectionName.EAST, d);

            d = Direction.TurnLeft(d);
            Assert.AreEqual(Direction.DirectionName.NORTH, d);

            d = Direction.TurnLeft(d);
            Assert.AreEqual(Direction.DirectionName.WEST, d);
        }

        [TestMethod]
        public void Right()
        {
            var d = Direction.DirectionName.WEST;

            d = Direction.TurnRight(d);
            Assert.AreEqual(Direction.DirectionName.NORTH, d);

            d = Direction.TurnRight(d);
            Assert.AreEqual(Direction.DirectionName.EAST, d);

            d = Direction.TurnRight(d);
            Assert.AreEqual(Direction.DirectionName.SOUTH, d);

            d = Direction.TurnRight(d);
            Assert.AreEqual(Direction.DirectionName.WEST, d);
        }
    }
}

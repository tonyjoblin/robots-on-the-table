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
            var d = Direction.DirectionName.LEFT;

            d = Direction.TurnLeft(d);
            Assert.AreEqual(Direction.DirectionName.DOWN, d);

            d = Direction.TurnLeft(d);
            Assert.AreEqual(Direction.DirectionName.RIGHT, d);

            d = Direction.TurnLeft(d);
            Assert.AreEqual(Direction.DirectionName.UP, d);

            d = Direction.TurnLeft(d);
            Assert.AreEqual(Direction.DirectionName.LEFT, d);
        }

        [TestMethod]
        public void Right()
        {
            var d = Direction.DirectionName.LEFT;

            d = Direction.TurnRight(d);
            Assert.AreEqual(Direction.DirectionName.UP, d);

            d = Direction.TurnRight(d);
            Assert.AreEqual(Direction.DirectionName.RIGHT, d);

            d = Direction.TurnRight(d);
            Assert.AreEqual(Direction.DirectionName.DOWN, d);

            d = Direction.TurnRight(d);
            Assert.AreEqual(Direction.DirectionName.LEFT, d);
        }
    }
}

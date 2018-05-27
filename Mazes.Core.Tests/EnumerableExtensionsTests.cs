using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Mazes.Core.Tests
{
    [TestClass()]
    public class EnumerableExtensionsTests
    {
        [TestMethod()]
        public void Random_WithCollection_ReturnsItem()
        {
            var ints = new List<int> { 1, 2, 3, 4, 5 };

            int randomInt = ints.Random();

            Assert.IsTrue(randomInt > 0 && randomInt < 6);
        }


        [TestMethod()]
        public void Random_WithEmptyCollection_ReturnsDefault()
        {
            var ints = new List<int>();

            int randomInt = ints.Random();

            Assert.IsTrue(randomInt == 0);
        }
    }
}
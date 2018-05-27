using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Mazes.Core.Tests
{
    [TestClass()]
    public class SideWinderTests
    {
        [TestMethod()]
        public void Generate_When5x5_GeneratesMazeWith25Cells()
        {
            var grid = new Grid(5, 5);
            SideWinder.Generate(grid);

            Assert.AreEqual(25, grid.Cells().Count());
        }

        [TestMethod()]
        public void Generate_When5x5_GeneratesMazeWithAtLeastOneLink()
        {
            var grid = new Grid(5, 5);
            SideWinder.Generate(grid);

            bool hasLink = false;
            foreach (Cell cell in grid.Cells())
            {
                if (cell.Links.Any())
                {
                    hasLink = true;
                    break;
                }
            }

            Assert.IsTrue(hasLink);
        }
    }
}
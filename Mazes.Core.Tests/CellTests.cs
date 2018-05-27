using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Mazes.Core.Tests
{
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void Link_IfAlreadyLinked_StaysLinked()
        {
            var cell00 = new Cell(0, 0);
            var cell01 = new Cell(0, 1);

            cell00.Link(cell01);
            cell00.Link(cell01);

            Assert.IsTrue(cell00.IsLinked(cell01));
        }

        [TestMethod]
        public void Link_WhenBidirectional_LinksBothCells()
        {
            var cell00 = new Cell(0, 0);
            var cell01 = new Cell(0, 1);

            cell00.Link(cell01);

            Assert.IsTrue(cell00.IsLinked(cell01));
            Assert.IsTrue(cell01.IsLinked(cell00));
        }

        [TestMethod]
        public void Link_WhenNotBidirectional_LinksOneCell()
        {
            var cell00 = new Cell(0, 0);
            var cell01 = new Cell(0, 1);

            cell00.Link(cell01, false);

            Assert.IsTrue(cell00.IsLinked(cell01));
            Assert.IsFalse(cell01.IsLinked(cell00));
        }

        [TestMethod]
        public void Links_WhenCellsAreLinked_ReturnsListOfLinkedCells()
        {
            var cell00 = new Cell(0, 0);
            var cell01 = new Cell(0, 1);

            cell00.Link(cell01);
            var linkedCells = cell00.Links.ToList();

            Assert.AreEqual(1, linkedCells.Count);
            Assert.AreEqual(0, linkedCells.First().Row);
            Assert.AreEqual(1, linkedCells.First().Column);
        }

        [TestMethod]
        public void Neighbors_WhenFourCellsAreNeighbors_ReturnsFourNeighbors()
        {
            var cell01 = new Cell(0, 1);
            var cell10 = new Cell(1, 0);
            var cell12 = new Cell(1, 2);
            var cell21 = new Cell(2, 1);
            var cell11 = new Cell(1, 1)
            {
                East = cell12,
                North = cell01,
                South = cell21,
                West = cell10
            };

            var neighbors = cell11.Neighbors.ToList();

            Assert.AreEqual(4, neighbors.Count);
        }

        [TestMethod]
        public void Neighbors_WhenNoCellsAreNeighbors_ReturnsEmptyList()
        {
            var cell11 = new Cell(1, 1);

            var neighbors = cell11.Neighbors.ToList();

            Assert.AreEqual(0, neighbors.Count);
        }

        [TestMethod]
        public void Unlink_IfAlreadyUnlinked_StaysUnlinked()
        {
            var cell00 = new Cell(0, 0);
            var cell01 = new Cell(0, 1);

            cell00.Link(cell01);
            cell01.Unlink(cell00);
            cell01.Unlink(cell00);

            Assert.IsFalse(cell00.IsLinked(cell01));
            Assert.IsFalse(cell01.IsLinked(cell00));
        }

        [TestMethod]
        public void Unlink_WhenBidirectional_UnlinksBothCells()
        {
            var cell00 = new Cell(0, 0);
            var cell01 = new Cell(0, 1);

            cell00.Link(cell01);
            cell01.Unlink(cell00);

            Assert.IsFalse(cell00.IsLinked(cell01));
            Assert.IsFalse(cell01.IsLinked(cell00));
        }

        [TestMethod]
        public void Unlink_WhenNotBidirectional_UnlinksOneCell()
        {
            var cell00 = new Cell(0, 0);
            var cell01 = new Cell(0, 1);

            cell00.Link(cell01);
            cell01.Unlink(cell00, false);

            Assert.IsTrue(cell00.IsLinked(cell01));
            Assert.IsFalse(cell01.IsLinked(cell00));
        }
    }
}

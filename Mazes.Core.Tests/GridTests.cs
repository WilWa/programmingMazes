using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Mazes.Core.Tests
{
    [TestClass()]
    public class GridTests
    {
        [TestMethod()]
        public void Grid_Constructor_CreatesrbycCells()
        {
            int columns = 5;
            int rows = 5;
            var grid = new Grid(rows, columns);

            Assert.AreEqual(rows * columns, grid.Size);
        }

        [TestMethod()]
        public void Grid_Constructor_CreatesCells()
        {
            int columns = 5;
            int rows = 5;
            var grid = new Grid(rows, columns);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    Assert.AreEqual(c, grid[r, c].Column);
                    Assert.AreEqual(r, grid[r, c].Row);
                }
            }
        }

        [TestMethod()]
        public void Grid_Constructor_CreatesCellNeighbors()
        {
            int columns = 5;
            int rows = 5;
            var grid = new Grid(rows, columns);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    if (c >= columns - 1)
                    {
                        Assert.IsNull(grid[r, c].East);
                    }
                    else
                    {
                        Assert.AreEqual(c + 1, grid[r, c].East.Column);
                        Assert.AreEqual(r, grid[r, c].East.Row);
                    }
                    if (r == 0)
                    {
                        Assert.IsNull(grid[r, c].North);
                    }
                    else
                    {
                        Assert.AreEqual(c, grid[r, c].North.Column);
                        Assert.AreEqual(r - 1, grid[r, c].North.Row);
                    }
                    if (r >= rows - 1)
                    {
                        Assert.IsNull(grid[r, c].South);
                    }
                    else
                    {
                        Assert.AreEqual(c, grid[r, c].South.Column);
                        Assert.AreEqual(r + 1, grid[r, c].South.Row);
                    }
                    if (c == 0)
                    {
                        Assert.IsNull(grid[r, c].West);
                    }
                    else
                    {
                        Assert.AreEqual(c - 1, grid[r, c].West.Column);
                        Assert.AreEqual(r, grid[r, c].West.Row);
                    }
                }
            }
        }

        [TestMethod]
        public void Cells_ReturnsCorrectCountOfCells()
        {
            int columns = 5;
            int rows = 5;
            var grid = new Grid(rows, columns);
            int count = 0;

            foreach (Cell cell in grid.Cells())
            {
                Assert.IsInstanceOfType(cell, typeof(Cell));
                count++;
            }
            Assert.AreEqual(columns * rows, count);
        }

        [TestMethod]
        public void RandomCell_ReturnsCell()
        {
            int columns = 5;
            int rows = 5;
            var grid = new Grid(rows, columns);

            Cell randomCell = grid.RandomCell;

            Assert.IsNotNull(randomCell);
            Assert.IsInstanceOfType(randomCell, typeof(Cell));
        }

        [TestMethod]
        public void Rows_ReturnsCorrectCountsOfCollectionCells()
        {
            int columns = 5;
            int rows = 5;
            var grid = new Grid(rows, columns);
            int cellCount = 0;
            int rowCount = 0;

            foreach (IEnumerable<Cell> cells in grid.Rows())
            {
                Assert.IsInstanceOfType(cells, typeof(IEnumerable<Cell>));
                rowCount++;
                foreach (Cell cell in cells)
                {
                    Assert.IsInstanceOfType(cell, typeof(Cell));
                    cellCount++;
                }
            }
            Assert.AreEqual(rows, rowCount);
            Assert.AreEqual(columns * rows, cellCount);
        }

        [TestMethod]
        public void ToBitmap_ReturnsBitmap()
        {
            int columns = 25;
            int rows = 25;
            var grid = new Grid(rows, columns);
            SideWinder.Generate(grid);

            var bitmap = grid.ToBitmap();

            Assert.IsNotNull(bitmap);
        }

        [TestMethod]
        public void ToString_ReturnsString()
        {
            int columns = 25;
            int rows = 25;
            var grid = new Grid(rows, columns);
            SideWinder.Generate(grid);

            string gridString = grid.ToString();

            Assert.IsTrue(!String.IsNullOrEmpty(gridString));
            Assert.IsTrue(!gridString.Contains("?"));
        }
    }
}

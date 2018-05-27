using System;
using System.Collections.Generic;
using System.Drawing;

namespace Mazes.Core
{
    public class Grid
    {
        private readonly Cell[,] _cells;
        private static Random _random = new Random();

        public Grid(int rows, int columns)
        {
            ColumnCount = columns;
            RowCount = rows;
            _cells = InitializeCells(rows, columns);
            ConfigureNeighbors(_cells);
        }

        public int ColumnCount { get; set; }
        public int RowCount { get; set; }

        public Cell RandomCell
        {
            get
            {
                int column = _random.Next(ColumnCount);
                int row = _random.Next(RowCount);
                return this[row, column];
            }
        }

        public int Size => _cells.Length;

        public Cell this[int row, int column]
        {
            get
            {
                if (row < 0 || row > RowCount - 1)
                {
                    return null;
                }
                if (column < 0 || column > ColumnCount - 1)
                {
                    return null;
                }
                return _cells[row, column];
            }
        }

        public IEnumerable<Cell> Cells()
        {
            for (int r = 0; r < RowCount; r++)
            {
                for (int c = 0; c < ColumnCount; c++)
                {
                    yield return _cells[r, c];
                }
            }
        }

        public Bitmap ToBitmap()
        {
            return GridPrinter.GenerateBitmap(this);
        }

        public override string ToString()
        {
            return GridPrinter.GenerateString(this);
        }

        private void ConfigureNeighbors(Cell[,] cells)
        {
            foreach (Cell cell in cells)
            {
                cell.East = this[cell.Row, cell.Column + 1];
                cell.North = this[cell.Row - 1, cell.Column];
                cell.South = this[cell.Row + 1, cell.Column];
                cell.West = this[cell.Row, cell.Column - 1];
            }
        }

        public IEnumerable<IEnumerable<Cell>> Rows()
        {
            for (int r = 0; r < RowCount; r++)
            {
                var cells = new List<Cell>();
                for (int c = 0; c < ColumnCount; c++)
                {
                    cells.Add(_cells[r, c]);
                }
                yield return cells;
            }
        }

        private static Cell[,] InitializeCells(int rows, int columns)
        {
            var cells = new Cell[rows, columns];
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    cells[r, c] = new Cell(r, c);
                }
            }
            return cells;
        }
    }
}

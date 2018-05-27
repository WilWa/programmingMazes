using System.Collections.Generic;

namespace Mazes.Core
{
    public class Cell
    {
        private readonly Dictionary<Cell, bool> _links;

        public Cell(int row, int column)
        {
            Column = column;
            Row = row;
            _links = new Dictionary<Cell, bool>();
        }

        public int Column { get; set; }
        public int Row { get; set; }

        public Cell East { get; set; }
        public Cell North { get; set; }
        public Cell South { get; set; }
        public Cell West { get; set; }

        public IEnumerable<Cell> Links => _links.Keys;

        public IEnumerable<Cell> Neighbors
        {
            get
            {
                var neighbors = new List<Cell>();
                if (East != null)
                {
                    neighbors.Add(East);
                }
                if (North != null)
                {
                    neighbors.Add(North);
                }
                if (South != null)
                {
                    neighbors.Add(South);
                }
                if (West != null)
                {
                    neighbors.Add(West);
                }
                return neighbors;
            }
        }

        public bool IsLinked(Cell cell)
        {
            return cell != null && _links.ContainsKey(cell) && _links[cell];
        }

        public void Link(Cell cell, bool bidirectional = true)
        {
            if (!_links.ContainsKey(cell))
            {
                _links.Add(cell, true);
            }
            if (bidirectional)
            {
                cell.Link(this, false);
            }
        }

        public void Unlink(Cell cell, bool bidirectional = true)
        {
            if (_links.ContainsKey(cell))
            {
                _links.Remove(cell);
            }
            if (bidirectional)
            {
                cell.Unlink(this, false);
            }
        }
    }
}
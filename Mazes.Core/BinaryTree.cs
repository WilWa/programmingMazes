using System.Collections.Generic;
using System.Linq;

namespace Mazes.Core
{
    public class BinaryTree
    {
        public static void Generate(Grid grid)
        {
            foreach (Cell cell in grid.Cells())
            {
                var northEastNeighbors = new List<Cell>();
                if (cell.East != null)
                {
                    northEastNeighbors.Add(cell.East);
                }
                if (cell.North != null)
                {
                    northEastNeighbors.Add(cell.North);
                }
                if (northEastNeighbors.Any())
                {
                    cell.Link(northEastNeighbors.Random());
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace Mazes.Core
{
    public class SideWinder
    {
        private static Random random = new Random();

        public static void Generate(Grid grid)
        {
            foreach (IEnumerable<Cell> row in grid.Rows())
            {
                var run = new List<Cell>();

                foreach (Cell cell in row)
                {
                    run.Add(cell);

                    bool atEasternBoundary = cell.East == null;
                    bool atNorthernBoundary = cell.North == null;

                    bool shouldCloseOut = atEasternBoundary || (!atNorthernBoundary && random.Next(2) == 0);

                    if (shouldCloseOut)
                    {
                        Cell member = run.Random();
                        if (member.North != null)
                        {
                            member.Link(member.North);
                        }
                        run.Clear();
                    }
                    else
                    {
                        cell.Link(cell.East);
                    }
                }
            }
        }
    }
}

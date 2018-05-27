using Mazes.Core;
using System.Windows;
using System.Windows.Media;

namespace Mazes.Wpf.Model
{
    internal class MazeMaker
    {
        public static Geometry MakeMazeGeometry(Algorithm algorithm, int rows, int columns)
        {
            var grid = new Grid(rows, columns);

            GenerateGrid(algorithm, grid);

            GeometryGroup mazeGeometry = GenerateGeometry(grid);

            return mazeGeometry;
        }

        private static GeometryGroup GenerateGeometry(Grid grid)
        {
            int cellSize = 10;
            var mazeGeometry = new GeometryGroup();
            foreach (Cell cell in grid.Cells())
            {
                int x1 = cell.Column * cellSize;
                int y1 = cell.Row * cellSize;
                int x2 = (cell.Column + 1) * cellSize;
                int y2 = (cell.Row + 1) * cellSize;

                if (!cell.IsLinked(cell.North))
                {
                    mazeGeometry.Children.Add(new LineGeometry(new Point(x1, y1), new Point(x2, y1)));
                }
                if (!cell.IsLinked(cell.West))
                {
                    mazeGeometry.Children.Add(new LineGeometry(new Point(x1, y1), new Point(x1, y2)));
                }
                if (!cell.IsLinked(cell.East))
                {
                    mazeGeometry.Children.Add(new LineGeometry(new Point(x2, y1), new Point(x2, y2)));
                }
                if (!cell.IsLinked(cell.South))
                {
                    mazeGeometry.Children.Add(new LineGeometry(new Point(x1, y2), new Point(x2, y2)));
                }
            }

            return mazeGeometry;
        }

        private static void GenerateGrid(Algorithm algorithm, Grid grid)
        {
            switch (algorithm)
            {
                case Algorithm.BinaryTree:
                    BinaryTree.Generate(grid);
                    break;
                case Algorithm.Sidewinder:
                    SideWinder.Generate(grid);
                    break;
            }
        }
    }
}

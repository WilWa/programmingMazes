using System.Drawing;
using System.Text;

namespace Mazes.Core
{
    internal class GridPrinter
    {
        private const char Down = '╷';
        private const char DownAndLeft = '┐';
        private const char DownAndRight = '┌';
        private const char Empty = ' ';
        private const char Horizontal = '─';
        private const char HorizontalAndDown = '┬';
        private const char HorizontalAndUp = '┴';
        private const char HorizontalAndVertical = '┼';
        private const char Left = '╴';
        private const char Right = '╶';
        private const char Up = '╵';
        private const char UpAndLeft = '┘';
        private const char UpAndRight = '└';
        private const char Vertical = '│';
        private const char VerticalAndLeft = '┤';
        private const char VerticalAndRight = '├';

        public static Bitmap GenerateBitmap(Grid grid, int cellSize = 10)
        {
            var bitmap = new Bitmap(cellSize * grid.ColumnCount + 1, cellSize * grid.RowCount + 1);
            using (var graphic = Graphics.FromImage(bitmap))
            {
                graphic.Clear(Color.White);

                foreach (Cell cell in grid.Cells())
                {
                    int x1 = cell.Column * cellSize;
                    int y1 = cell.Row * cellSize;
                    int x2 = (cell.Column + 1) * cellSize;
                    int y2 = (cell.Row + 1) * cellSize;

                    Pen pen = Pens.Black;
                    if (!cell.IsLinked(cell.North))
                    {
                        graphic.DrawLine(pen, new Point(x1, y1), new Point(x2, y1));
                    }
                    if (!cell.IsLinked(cell.West))
                    {
                        graphic.DrawLine(pen, new Point(x1, y1), new Point(x1, y2));
                    }
                    if (!cell.IsLinked(cell.East))
                    {
                        graphic.DrawLine(pen, new Point(x2, y1), new Point(x2, y2));
                    }
                    if (!cell.IsLinked(cell.South))
                    {
                        graphic.DrawLine(pen, new Point(x1, y2), new Point(x2, y2));
                    }
                }

                graphic.Save();
            }

            return bitmap;
        }

        public static string GenerateString(Grid grid, int horizontalRepeat = 3, int verticalRepeat = 2)
        {
            var output = new StringBuilder();

            // Top
            output.Append(DownAndRight);
            for (int col = 0; col < grid.ColumnCount; col++)
            {
                output.Append(Horizontal, horizontalRepeat);
                if (col < grid.ColumnCount - 1)
                {
                    if (grid[0, col].IsLinked(grid[0, col + 1]))
                    {
                        output.Append(Horizontal);
                    }
                    else
                    {
                        output.Append(HorizontalAndDown);
                    }
                }
                else
                {
                    output.Append(DownAndLeft);
                }
            }
            output.AppendLine();

            for (int row = 0; row < grid.RowCount; row++)
            {
                for (int height = 0; height < verticalRepeat; height++)
                {
                    for (int col = 0; col < grid.ColumnCount; col++)
                    {
                        // Leftmost column
                        if (col == 0)
                        {
                            if (height == verticalRepeat - 1 && row == grid.RowCount - 1)
                            {
                                output.Append(UpAndRight);
                            }
                            else if (height < verticalRepeat - 1 || grid[row, col].IsLinked(grid[row + 1, col]))
                            {
                                output.Append(Vertical);
                            }
                            else
                            {
                                output.Append(VerticalAndRight);
                            }
                        }

                        // Right side
                        if (height < verticalRepeat - 1)
                        {
                            output.Append(Empty, horizontalRepeat);
                            if (grid[row, col].IsLinked(grid[row, col + 1]))
                            {
                                output.Append(Empty);
                            }
                            else
                            {
                                output.Append(Vertical);
                            }
                        }

                        // Bottom side
                        if (height == verticalRepeat - 1)
                        {
                            bool nwneLinked = grid[row, col].IsLinked(grid[row, col + 1]);
                            bool nwswLinked = grid[row, col].IsLinked(grid[row + 1, col]);
                            bool swseLinked = grid[row + 1, col] != null && grid[row + 1, col].IsLinked(grid[row + 1, col + 1]);
                            bool neseLinked = grid[row, col + 1] != null && grid[row, col + 1].IsLinked(grid[row + 1, col + 1]);

                            if (nwswLinked)
                            {
                                output.Append(Empty, horizontalRepeat);
                            }
                            else
                            {
                                output.Append(Horizontal, horizontalRepeat);
                            }

                            // Bottom right corner
                            output.Append(GetBottomRightCorner(row, col, grid.RowCount, grid.ColumnCount, nwneLinked, nwswLinked, swseLinked, neseLinked));
                        }
                    }
                    output.AppendLine();
                }
            }

            return output.ToString();
        }

        private static char GetBottomRightCorner(int row, int col, int rowCount, int columnCount, bool nwneLinked, bool nwswLinked, bool swseLinked, bool neseLinked)
        {
            if (row == rowCount - 1 && col == columnCount - 1)
            {
                return UpAndLeft;
            }
            else if (row == rowCount - 1)
            {
                if (nwneLinked)
                {
                    return Horizontal;
                }
                else
                {
                    return HorizontalAndUp;
                }
            }
            else if (col == columnCount - 1)
            {
                if (nwswLinked)
                {
                    return Vertical;
                }
                else
                {
                    return VerticalAndLeft;
                }
            }
            else
            {
                if (!nwneLinked && !nwswLinked && !swseLinked && !neseLinked)
                {
                    return HorizontalAndVertical;
                }
                else if (!nwneLinked && !nwswLinked && !swseLinked)
                {
                    return VerticalAndLeft;
                }
                else if (!nwneLinked && !nwswLinked && !neseLinked)
                {
                    return HorizontalAndUp;
                }
                else if (!nwneLinked && !swseLinked && !neseLinked)
                {
                    return VerticalAndRight;
                }
                else if (!nwswLinked && !swseLinked && !neseLinked)
                {
                    return HorizontalAndDown;
                }
                else if (!nwneLinked && !nwswLinked)
                {
                    return UpAndLeft;
                }
                else if (!swseLinked && !neseLinked)
                {
                    return DownAndRight;
                }
                else if (!nwneLinked && !swseLinked)
                {
                    return Vertical;
                }
                else if (!nwswLinked && !neseLinked)
                {
                    return Horizontal;
                }
                else if (!nwneLinked && !neseLinked)
                {
                    return UpAndRight;
                }
                else if (!nwswLinked && !swseLinked)
                {
                    return DownAndLeft;
                }
                else if (!nwneLinked)
                {
                    return Up;
                }
                else if (!nwswLinked)
                {
                    return Left;
                }
                else if (!swseLinked)
                {
                    return Down;
                }
                else if (!neseLinked)
                {
                    return Right;
                }
            }
            return '?';
        }
    }
}

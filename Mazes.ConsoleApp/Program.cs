using Mazes.Core;
using System;

namespace Mazes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var grid = new Grid(10, 10);
            SideWinder.Generate(grid);
            Console.Write(grid);
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}

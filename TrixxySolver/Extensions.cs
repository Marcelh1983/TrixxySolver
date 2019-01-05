using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzleResolver
{
    public static class EnumerableExtensions
    {
        public static ConsoleColor GetConsoleColor(this Color color)
        {
            switch (color)
            {
                case Color.green:
                    return ConsoleColor.Green;
                case Color.purple:
                    return ConsoleColor.DarkMagenta;
                case Color.red:
                    return ConsoleColor.Red;
                case Color.transperent:
                    return ConsoleColor.White;
                case Color.yellow:
                    return ConsoleColor.Yellow;
                default: return ConsoleColor.Black;
            }
        }

        public static void ToConsole(this Color[,] colors)
        {
            var rowSize = 3;
            var columnSize = 6;
            for (int x = 0; x < 4; x++)
            {
                for (int r = 0; r < rowSize; r++)
                {
                    Console.WriteLine();
                    for (int y = 0; y < 4; y++)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = colors[x, y].GetConsoleColor();
                        Console.Write(r == (rowSize -1) ? '_'.Multiply(columnSize) : ' '.Multiply(columnSize));
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(" ");
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }

        public static string Multiply(this char input, int multiplier)
        {
            var sb = new StringBuilder("");
            for (int i = 0; i < multiplier; i++)
            {
                sb.Append(input);
            }
            return sb.ToString();
        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                var itemAsEnumerable = Enumerable.Repeat(item, 1);
                var subSet = items.Except(itemAsEnumerable);
                if (!subSet.Any())
                {
                    yield return itemAsEnumerable;
                }
                else
                {
                    foreach (var sub in items.Except(itemAsEnumerable).GetPermutations())
                    {
                        yield return itemAsEnumerable.Union(sub);
                    }
                }
            }
        }
    }
}

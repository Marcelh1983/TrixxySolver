using PuzzleResolver.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrixxySolver.Model;

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

        public static void ToConsole(this CardsInStack c)
        {
            // output the right combination.
            Console.WriteLine($"Two sides correct! {Environment.NewLine}" +
                $"Card 1: {c.Card1.Card.Name} rotation: {c.Card1.Rotation} front: {c.Card1.IsFront}{Environment.NewLine}" +
                $"Card 2: {c.Card2.Card.Name} rotation: {c.Card2.Rotation} front: {c.Card2.IsFront}{Environment.NewLine}" +
                $"Card 3: {c.Card3.Card.Name} rotation: {c.Card3.Rotation} front: {c.Card3.IsFront}{Environment.NewLine}" +
                $"Card 4: {c.Card4.Card.Name} rotation: {c.Card4.Rotation} front: {c.Card4.IsFront}{Environment.NewLine}");
            Console.WriteLine("Card 1");
            c.Card1.Colors.ToConsole();
            Console.WriteLine();

            Console.WriteLine("Card 2");
            c.Card2.Colors.ToConsole();
            Console.WriteLine();

            Console.WriteLine("Card 3");
            c.Card3.Colors.ToConsole();
            Console.WriteLine();

            Console.WriteLine("Card 4");
            c.Card4.Colors.ToConsole();
            Console.WriteLine();
        }

        public static List<CardsInStack> GetPossibleStacks(this IEnumerable<IEnumerable<Card>> listOfCardLists)
        {
            var possibleStacks = new List<CardsInStack>();
            listOfCardLists.ToList().ForEach(listOfCards =>
            {
                // we don't rotate the first card. Since the rotation 
                // 0, 1, 2, 3 is basically equal to 1, 2, 3, 0
                // The rest of the cards we do rotate.
                listOfCards.ElementAt(0).GetCombinations(false).ToList().ForEach(c1 =>
                {
                    listOfCards.ElementAt(1).GetCombinations().ToList().ForEach(c2 =>
                    {
                        listOfCards.ElementAt(2).GetCombinations().ToList().ForEach(c3 =>
                        {
                            listOfCards.ElementAt(3).GetCombinations().ToList().ForEach(c4 =>
                            {

                                possibleStacks.Add(new CardsInStack
                                {
                                    Card1 = c1,
                                    Card2 = c2,
                                    Card3 = c3,
                                    Card4 = c4
                                });
                            });
                        });
                    });
                });
            });
            return possibleStacks;
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

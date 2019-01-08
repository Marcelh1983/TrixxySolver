using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
namespace PuzzleResolver
{
    class Program
    {
        static void Main(string[] args)
        {
            SetConsoleSize();
            // Get cards that are hard coded in the GetCards function
            var cards = GetCards();
            // we can check half of the permutation to find the combinations
            // because we are looking for a solution where front and back are correct.
            // if we would check all permutions we would find the correct solution twice (one the reverse of the other)
            var possibleStacks = cards
                .GetPermutations().Where(p => p.First().Name == "Card 1" || p.First().Name == "Card 2")
                .GetPossibleStacks();
            // check the front and back side of all combinations
            possibleStacks.Where(s => s.CheckFront() && s.CheckBack())
                .ToList()
                .ForEach(s => s.ToConsole());
            Console.WriteLine();
            Console.WriteLine("Done!");
            Console.ReadKey();
        }
        private static void SetConsoleSize()
        {
            try
            {
                Console.WindowWidth = 150;
                Console.WindowHeight = 50;
            }
            catch (Exception)
            {
                // ignore
            }
        }
        private static List<Card> GetCards()
        {
            // define cards
            var card1 = new Card()
            {
                Name = "Card 1",
                front = new Color[4, 4] {
                { Color.transperent,Color.transperent,Color.transperent,Color.transperent },
                { Color.purple,Color.green,Color.transperent,Color.transperent },
                { Color.purple,Color.transperent,Color.red,Color.transperent },
                { Color.purple,Color.transperent,Color.red,Color.yellow }
                },
                back = new Color[4, 4]
    {
                { Color.transperent,Color.transperent,Color.transperent,Color.transperent },
                { Color.transperent,Color.transperent,Color.red,Color.red },
                { Color.transperent,Color.green,Color.transperent,Color.green },
                { Color.yellow,Color.yellow,Color.transperent,Color.yellow }
                }
            };
            var card2 = new Card()
            {
                Name = "Card 2",
                front = new Color[4, 4] {
                { Color.transperent,Color.red,Color.green,Color.transperent },
                { Color.yellow,Color.red,Color.transperent,Color.purple },
                { Color.yellow,Color.transperent,Color.green,Color.transperent },
                { Color.transperent,Color.red,Color.transperent,Color.purple }
                },
                back = new Color[4, 4]
    {
                { Color.transperent,Color.red,Color.yellow,Color.transperent },
                { Color.purple,Color.transperent,Color.yellow,Color.green },
                { Color.transperent,Color.red,Color.transperent,Color.green },
                { Color.purple,Color.transperent,Color.yellow,Color.transperent }
               }
            };
            var card3 = new Card()
            {
                Name = "Card 3",
                front = new Color[4, 4] {
                { Color.yellow,Color.transperent,Color.transperent,Color.purple },
                { Color.yellow,Color.green,Color.transperent,Color.transperent },
                { Color.yellow,Color.transperent,Color.red,Color.transperent },
                { Color.yellow,Color.transperent,Color.transperent,Color.purple }
                },
                back = new Color[4, 4]
    {
                { Color.purple,Color.transperent,Color.transperent,Color.purple },
                { Color.transperent,Color.transperent,Color.red,Color.red },
                { Color.transperent,Color.green,Color.transperent,Color.green },
                { Color.yellow,Color.transperent,Color.transperent,Color.yellow }
               }
            };
            var card4 = new Card()
            {
                Name = "Card 4",
                front = new Color[4, 4] {
                { Color.transperent,Color.red,Color.green,Color.purple },
                { Color.yellow,Color.red,Color.transperent,Color.purple },
                { Color.yellow,Color.transperent,Color.green,Color.purple },
                { Color.transperent,Color.transperent,Color.green,Color.transperent }
                },
                back = new Color[4, 4]
    {
                { Color.purple,Color.purple,Color.purple,Color.transperent },
                { Color.red,Color.transperent,Color.red,Color.red },
                { Color.green,Color.green,Color.transperent,Color.green },
                { Color.transperent,Color.yellow,Color.transperent,Color.transperent }
               }
            };
            return new List<Card> { card1, card2, card3, card4 };
        }
    }
}
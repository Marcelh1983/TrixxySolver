using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using PuzzleResolver.Model;

namespace PuzzleResolver
{
    class Program
    {
        static void Main(string[] args)
        {
            SetConsoleSize();
            // Get cards that are hard coded in the GetCards function
            // we can check half of the permutation to find the combinations
            // because we are looking for a solution where front and back are correct.
            // if we would check all permutions we would find the correct solution twice (one the reverse of the other)
            Card.GetCards()
                .GetPermutations()
                .Where(p => p.First().Name == "Card 1" || p.First().Name == "Card 2")
                .GetPossibleStacks()
                .Where(s => s.CheckFront() && s.CheckBack())
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
            {                 // ignore
            }
        }

    }
}
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
            var sw = new Stopwatch();
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
            Console.WindowWidth = 150;
            Console.WindowHeight = 50;
            Console.WriteLine("Generate combinations");
            sw.Start();
            var cards = new List<Card> { card1, card2, card3, card4 };
            var combinations = new List<CardsInStack>();
            var permutations = cards.GetPermutations().Where(p => p.First().Name == "Card 1" || p.First().Name == "Card 2")
                .ToList();
            permutations.ForEach(listOfCards =>
            {
                listOfCards.ElementAt(0).GetCombinations(false).ToList().ForEach(c1 =>
                {
                    listOfCards.ElementAt(1).GetCombinations().ToList().ForEach(c2 =>
                    {
                        listOfCards.ElementAt(2).GetCombinations().ToList().ForEach(c3 =>
                        {
                            listOfCards.ElementAt(3).GetCombinations().ToList().ForEach(c4 =>
                            {

                                combinations.Add( new CardsInStack
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
            Console.WriteLine($"Generated combinations in {sw.Elapsed.Seconds} seconds");
            sw.Restart();
            Console.WriteLine("Checking combinations");
            var twoSidesCorrectList = new List<CardsInStack>();
            combinations.ToList().ForEach(c =>
            {
                if (c.CheckFront())
                {
                    if (c.CheckBack())
                    {
                        twoSidesCorrectList.Add(c);
                    } 
                }
            });
            Console.WriteLine($"Checked combinations in {sw.Elapsed.Seconds} seconds");
            twoSidesCorrectList.ForEach(c =>
            {
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
            });
            Console.WriteLine();
            Console.WriteLine("Done!");
            Console.ReadKey();
        }
       
    }
}
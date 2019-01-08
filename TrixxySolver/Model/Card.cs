using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrixxySolver.Model;

namespace PuzzleResolver.Model
{
    public class Card
    {
        public Color[,] front;
        public Color[,] back;
        public string Name;
        public Color[,] GetCard(bool isFront, int rotate) =>
            RotateCard(isFront ? front : back, rotate);

        public IEnumerable<CardInStack> GetCombinations(bool shouldRotate = true) =>
            new List<string> { "front", "back" }
                .SelectMany(side =>
                    Enumerable.Range(0, shouldRotate ? 4 : 1).Select(rotateIndex =>
                        new CardInStack
                        {
                            IsFront = side == "front",
                            Card = this,
                            Rotation = rotateIndex,
                            Colors = RotateCard(side == "front" ? front : back, rotateIndex)
                        }
                    ));
        static Color[,] RotateCardCounterClockwise(Color[,] oldCart)
        {
            Color[,] newCart = new Color[oldCart.GetLength(1), oldCart.GetLength(0)];
            int newColumn, newRow = 0;
            for (int oldColumn = oldCart.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
            {
                newColumn = 0;
                for (int oldRow = 0; oldRow < oldCart.GetLength(0); oldRow++)
                {
                    newCart[newRow, newColumn] = oldCart[oldRow, oldColumn];
                    newColumn++;
                }
                newRow++;
            }
            return newCart;
        }
        
        static Color[,] RotateCard(Color[,] card, int n)
        {
            Color[,] ret = card;
            for (int i = 0; i < n; ++i)
            {
                ret = RotateCardCounterClockwise(ret);
            }
            return ret;
        }

        public static IEnumerable<Card> GetCards()
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

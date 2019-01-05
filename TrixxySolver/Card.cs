using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzleResolver
{
    public class Card
    {
        public Color[,] front;
        public Color[,] back;
        public string Name;
        public Color[,] GetCard(bool isFront, int rotate)
        {
            return RotateMatrix(isFront ? front : back, rotate);
        }

        public List<CardInStack> GetCombinations(bool shouldRotate = true)
        {
            var list = new List<CardInStack>();
            new List<string> { "front", "back" }.ForEach(side =>
            {
                for (int rotateIndex = 0; rotateIndex < (shouldRotate ? 4 : 1); rotateIndex++)
                {
                    var list2 = side == "front" ? front : back;
                    if (rotateIndex != 0)
                    {
                        list2 = RotateMatrix(list2, rotateIndex);
                    }
                    list.Add(new CardInStack
                    {
                        IsFront = side == "front",
                        Card = this,
                        Rotation = rotateIndex,
                        Colors = list2
                    });
                }
            });
            return list;
        }
        //Color[,] rotated = RotateMatrix(colors, 4);
        static Color[,] RotateMatrixCounterClockwise(Color[,] oldMatrix)
        {
            Color[,] newMatrix = new Color[oldMatrix.GetLength(1), oldMatrix.GetLength(0)];
            int newColumn, newRow = 0;
            for (int oldColumn = oldMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
            {
                newColumn = 0;
                for (int oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++)
                {
                    newMatrix[newRow, newColumn] = oldMatrix[oldRow, oldColumn];
                    newColumn++;
                }
                newRow++;
            }
            return newMatrix;
        }


        static Color[,] RotateMatrix(Color[,] matrix, int n)
        {
            Color[,] ret = matrix;
            for (int i = 0; i < n; ++i)
            {
                ret = RotateMatrixCounterClockwise(ret);
            }
            return ret;
        }
        static Color[,] Rotate(Color[,] matrix, int n)
        {
            Color[,] ret = new Color[n, n];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    ret[i, j] = matrix[n - j - 1, i];
                }
            }

            return ret;
        }
    }

    public enum Color
    {
        red = 0,
        yellow = 1,
        purple = 2,
        green = 3,
        transperent = 4
    }

    public class CardInStack
    {
        public Card Card;
        public int Rotation;
        public bool IsFront;
        public Color[,] Colors;
    }
    public class CardsInStack
    {
        public CardInStack Card1;
        public CardInStack Card2;
        public CardInStack Card3;
        public CardInStack Card4;

        public bool CheckFront() =>
            CheckStackOneSide(new List<Color[,]>
                {
                    Card1.Colors,
                    Card2.Colors,
                    Card3.Colors,
                    Card4.Colors
                }, true);

        public bool CheckBack() =>
            CheckStackOneSide(new List<Color[,]>
                {
                    Card4.Card.GetCard(!Card4.IsFront, Card4.Rotation),
                    Card3.Card.GetCard(!Card3.IsFront, Card3.Rotation),
                    Card2.Card.GetCard(!Card2.IsFront, Card2.Rotation),
                    Card1.Card.GetCard(!Card1.IsFront, Card1.Rotation)
                }, true);
        private static bool CheckStackOneSide(List<Color[,]> cards, bool noGaps)
        {
            var total = (Color[,])cards[0].Clone();
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    var color = total[x, y];
                    var getNextIndex = 1;
                    if (color == Color.transperent)
                    {
                        while (color == Color.transperent && getNextIndex < cards.Count)
                        {
                            color = cards[getNextIndex][x, y];
                            getNextIndex++;
                        }
                        total[x, y] = color;
                    }
                }
            }
            // vertical and check horizontal
           return Check(total, true, noGaps) || Check(total, false, noGaps);
        }

        private static bool Check(Color[,] card, bool checkRow, bool checkComplete = false)
        {
            for (int i = 0; i < 4; i++)
            {
                var row = new List<Color> {
                card[checkRow ? i : 0,checkRow ? 0 : i],
                card[checkRow ? i : 1,checkRow ? 1 : i],
                card[checkRow ? i : 2,checkRow ? 2 : i],
                card[checkRow ? i : 3,checkRow ? 3 : i]
                };
                var colors = row.Distinct();
                if (!(colors.Count() == 1) &&
                    !(colors.Count() == 2 && !checkComplete && colors.Any(c => c == Color.transperent)))
                {
                    return false;
                };
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrixxySolver.Model
{
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
            for (int x = 0; x < cards[0].GetLength(0); x++)
            {
                for (int y = 0; y < cards[0].GetLength(1); y++)
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
            for (int i = 0; i < card.GetLength(checkRow ? 0 : 1); i++)
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

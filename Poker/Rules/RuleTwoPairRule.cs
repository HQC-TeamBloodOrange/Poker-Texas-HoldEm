using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Rules
{
    public class RuleTwoPairRule
    {
        public static void FollowRule(ref double current, ref double Power, PokerForm form, int i)
        {
            if (current >= -1)
            {
                bool messageBox = false;

                int maxIndexOfCardInTable = 16;
                int minIndexOfCardInTable = 12;

                for (int cardInTableIndex = maxIndexOfCardInTable; cardInTableIndex >= minIndexOfCardInTable; cardInTableIndex--)
                {
                    int max = cardInTableIndex - minIndexOfCardInTable;

                    if (form.DrawnCards[i] / 4 != form.DrawnCards[i + 1] / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (cardInTableIndex - k < 12)
                            {
                                max--;
                            }

                            if (cardInTableIndex - k >= 12)
                            {
                                if (form.DrawnCards[i] / 4 == form.DrawnCards[cardInTableIndex] / 4 &&
                                    form.DrawnCards[i + 1] / 4 == form.DrawnCards[cardInTableIndex - k] / 4 ||
                                    form.DrawnCards[i + 1] / 4 == form.DrawnCards[cardInTableIndex] / 4 &&
                                    form.DrawnCards[i] / 4 == form.DrawnCards[cardInTableIndex - k] / 4)
                                {
                                    if (!messageBox)
                                    {
                                        if (form.DrawnCards[i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (form.DrawnCards[i + 1] / 4) * 2 + current * 100;
                                            form.Win.Add(new Type
                                            {
                                                Power = Power,
                                                Current = 2
                                            });
                                            form.Sorted = form.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (form.DrawnCards[i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (form.DrawnCards[i] / 4) * 2 + current * 100;
                                            form.Win.Add(new Type
                                            {
                                                Power = Power,
                                                Current = 2
                                            });
                                            form.Sorted = form.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (form.DrawnCards[i + 1] / 4 != 0 && form.DrawnCards[i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (form.DrawnCards[i] / 4) * 2 + (form.DrawnCards[i + 1] / 4) * 2 + current * 100;
                                            form.Win.Add(new Type
                                            {
                                                Power = Power,
                                                Current = 2
                                            });
                                            form.Sorted = form.Win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }
                                    }

                                    messageBox = true;
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}

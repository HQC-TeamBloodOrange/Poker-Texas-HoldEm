using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Rules
{
    public class PairTwoPairRule
    {
        public static void FollowRule(double current, double Power, PokerForm form, int i)
        {
            if (current >= -1)
            {
                bool mesaggeBox = false;
                bool otherMesaggeBox = false;

                int maxIndexOfCardInTable = 16;
                int minIndexOfCardInTable = 12;

                for (int cardsInTableIndex = maxIndexOfCardInTable; cardsInTableIndex >= minIndexOfCardInTable; cardsInTableIndex--)
                {
                    int max = cardsInTableIndex - minIndexOfCardInTable;

                    for (int k = 1; k <= max; k++)
                    {
                        if (cardsInTableIndex - k < 12)
                        {
                            max--;
                        }

                        if (cardsInTableIndex - k >= 12)
                        {
                            if (form.DrawnCards[cardsInTableIndex] / 4 == form.DrawnCards[cardsInTableIndex - k] / 4)
                            {
                                if (form.DrawnCards[cardsInTableIndex] / 4 != form.DrawnCards[i] / 4 &&
                                    form.DrawnCards[cardsInTableIndex] / 4 != form.DrawnCards[i + 1] / 4 &&
                                    current == 1)
                                {
                                    if (!mesaggeBox)
                                    {
                                        if (form.DrawnCards[i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (form.DrawnCards[i] / 4) * 2 + 13 * 4 + current * 100;
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

                                        if (form.DrawnCards[i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (form.DrawnCards[i + 1] / 4) * 2 + 13 * 4 + current * 100;
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

                                        if (form.DrawnCards[i + 1] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (form.DrawnCards[cardsInTableIndex] / 4) * 2 + (form.DrawnCards[i + 1] / 4) * 2 + current * 100;
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

                                        if (form.DrawnCards[i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (form.DrawnCards[cardsInTableIndex] / 4) * 2 + (form.DrawnCards[i] / 4) * 2 + current * 100;
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

                                    mesaggeBox = true;
                                }

                                if (current == -1)
                                {
                                    if (!otherMesaggeBox)
                                    {
                                        if (form.DrawnCards[i] / 4 > form.DrawnCards[i + 1] / 4)
                                        {
                                            if (form.DrawnCards[cardsInTableIndex] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + form.DrawnCards[i] / 4 + current * 100;
                                                form.Win.Add(new Type
                                                {
                                                    Power = Power,
                                                    Current = 1
                                                });
                                                form.Sorted = form.Win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = form.DrawnCards[cardsInTableIndex] / 4 + form.DrawnCards[i] / 4 + current * 100;
                                                form.Win.Add(new Type
                                                {
                                                    Power = Power,
                                                    Current = 1
                                                });
                                                form.Sorted = form.Win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                        }
                                        else
                                        {
                                            if (form.DrawnCards[cardsInTableIndex] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + form.DrawnCards[i + 1] + current * 100;
                                                form.Win.Add(new Type
                                                {
                                                    Power = Power,
                                                    Current = 1
                                                });
                                                form.Sorted = form.Win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = form.DrawnCards[cardsInTableIndex] / 4 + form.DrawnCards[i + 1] / 4 + current * 100;
                                                form.Win.Add(new Type
                                                {
                                                    Power = Power,
                                                    Current = 1
                                                });

                                                form.Sorted = form.Win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                        }
                                    }

                                    otherMesaggeBox = true;
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}

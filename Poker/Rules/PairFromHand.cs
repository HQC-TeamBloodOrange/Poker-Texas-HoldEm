using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Rules
{
    using System.Runtime.CompilerServices;

    public class PairFromHand
    {
        public static void FollowRule(double current, double Power, PokerForm form, int i)
        {

            {
                if (current >= -1)
                {
                    var msgbox = false;
                    if (form.DrawnCards[i] / 4 == form.DrawnCards[i + 1] / 4)
                    {
                        if (!msgbox)
                        {
                            if (form.DrawnCards[i] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + current * 100;

                                form.Win.Add(new Type { Power = Power, Current = 1 });
                                form.Sorted =
                                    form.Win.OrderByDescending(op => op.Current)
                                        .ThenByDescending(op => op.Power)
                                        .First();
                            }
                            else
                            {
                                current = 1;
                                Power = (form.DrawnCards[i + 1] / 4) * 4 + current * 100;
                                form.Win.Add(new Type { Power = Power, Current = 1 });
                                form.Sorted =
                                    form.Win.OrderByDescending(op => op.Current)
                                        .ThenByDescending(op => op.Power)
                                        .First();
                            }
                        }
                        msgbox = true;
                    }
                    for (var tc = 16; tc >= 12; tc--)
                    {
                        if (form.DrawnCards[i + 1] / 4 == form.DrawnCards[tc] / 4)
                        {
                            if (!msgbox)
                            {
                                if (form.i + 1 / 4 == 0)
                                {
                                    current = 1;
                                    Power = 13 * 4 + form.DrawnCards[i] / 4 + current * 100;
                                    form.Win.Add(new Type { Power = Power, Current = 1 });
                                    form.Sorted =
                                        form.Win.OrderByDescending(op => op.Current)
                                            .ThenByDescending(op => op.Power)
                                            .First();
                                }
                                else
                                {
                                    current = 1;
                                    Power = (form.DrawnCards[form.i + 1] / 4) * 4 + form.DrawnCards[form.i] / 4
                                            + current * 100;
                                    form.Win.Add(new Type { Power = Power, Current = 1 });
                                    form.Sorted =
                                        form.Win.OrderByDescending(op => op.Current)
                                            .ThenByDescending(op => op.Power)
                                            .First();
                                }
                            }
                            msgbox = true;
                        }
                        if (form.DrawnCards[form.i] / 4 == form.DrawnCards[tc] / 4)
                        {
                            if (!msgbox)
                            {
                                if (form.DrawnCards[form.i] / 4 == 0)
                                {
                                    current = 1;
                                    Power = 13 * 4 + form.DrawnCards[form.i + 1] / 4 + current * 100;
                                    form.Win.Add(new Type { Power = Power, Current = 1 });
                                    form.Sorted =
                                        form.Win.OrderByDescending(op => op.Current)
                                            .ThenByDescending(op => op.Power)
                                            .First();
                                }
                                else
                                {
                                    current = 1;
                                    Power = (form.DrawnCards[tc] / 4) * 4 + form.DrawnCards[form.i + 1] / 4
                                            + current * 100;
                                    form.Win.Add(new Type { Power = Power, Current = 1 });
                                    form.Sorted =
                                        form.Win.OrderByDescending(op => op.Current)
                                            .ThenByDescending(op => op.Power)
                                            .First();
                                }
                            }
                            msgbox = true;
                        }
                    }
                }
            }
        }
    }
}

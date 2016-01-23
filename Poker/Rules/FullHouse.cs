using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Rules
{
    public class FullHouse
    {
        public static void FollowRule(ref double current, ref double Power, ref bool done, int[] Straight, double type, PokerForm form, int i)
        {
            if (current >= -1)
            {
                type = Power;
                for (var j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                current = 6;
                                Power = 13 * 2 + current * 100;
                                form.Win.Add(new Type
                                {
                                    Power = Power,
                                    Current = 6
                                });
                                form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                            if (fh.Max() / 4 > 0)
                            {
                                current = 6;
                                Power = fh.Max() / 4 * 2 + current * 100;
                                form.Win.Add(new Type
                                {
                                    Power = Power,
                                    Current = 6
                                });
                                form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                        }

                        if (!done)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                Power = 13;
                                done = true;
                                j = -1;
                            }
                            else
                            {
                                Power = fh.Max() / 4;
                                done = true;
                                j = -1;
                            }
                        }
                    }
                }

                if (current != 6)
                {
                    Power = type;
                }
            }
        }

    }
}

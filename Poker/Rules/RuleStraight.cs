using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Rules
{
    public class RuleStraight
    {
        public static void FollowRule(ref double current, ref double Power, int[] cardsOnTableWithPlayerCards, PokerForm form, int i)
        {
            if (current >= -1)
            {
                var op = cardsOnTableWithPlayerCards.Select(o => o / 4).Distinct().ToArray();
                for (var j = 0; j < op.Length - 4; j++)
                {
                    if (op[j] + 4 == op[j + 4])
                    {
                        if (op.Max() - 4 == op[j])
                        {
                            current = 4;
                            Power = op.Max() + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 4
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                        else
                        {
                            current = 4;
                            Power = op[j + 4] + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 4
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                    }
                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        current = 4;
                        Power = 13 + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 4
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

    }
}

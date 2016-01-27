using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Rules
{
    public class RuleThreeOfAKind
    {
        public static void FollowRule(double current, double Power, int[] cardsOnTableWithPlayerCards, PokerForm form, int i)
        {
            if (current >= -1)
            {
                for (var j = 0; j <= 12; j++)
                {
                    var fh = cardsOnTableWithPlayerCards.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3)
                    {
                        if (fh.Max() / 4 == 0)
                        {
                            current = 3;
                            Power = 13 * 3 + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 3
                            });
                            form.Sorted = form.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 3;
                            Power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 3
                            });
                            form.Sorted = form.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                }
            }
        }

    }
}

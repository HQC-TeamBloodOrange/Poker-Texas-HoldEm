using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Rules
{
    public class FourOfAKind
    {
        public static void FollowRule(ref double current, ref double Power, int[] Straight, PokerForm form, int i)
        {
            if (current >= -1)
            {
                for (var j = 0; j <= 3; j++)
                {
                    if (Straight[j] / 4 == Straight[j + 1] / 4 && Straight[j] / 4 == Straight[j + 2] / 4 && Straight[j] / 4 == Straight[j + 3] / 4)
                    {
                        current = 7;
                        Power = (Straight[j] / 4) * 4 + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 7
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0 && Straight[j + 3] / 4 == 0)
                    {
                        current = 7;
                        Power = 13 * 4 + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 7
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

    }
}

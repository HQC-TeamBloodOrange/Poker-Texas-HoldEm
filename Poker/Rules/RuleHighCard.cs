using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Rules
{
    public class RuleHighCard
    {
        public static void FollowRule(double current, double Power, PokerForm form, int i)
        {
            if (current == -1)
            {
                if (form.DrawnCards[i] / 4 > form.DrawnCards[i + 1] / 4)
                {
                    current = -1;
                    Power = form.DrawnCards[i] / 4;

                    form.Win.Add(new Type
                    {
                        Power = Power,
                        Current = -1
                    });
                    form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                else
                {
                    current = -1;
                    Power = form.DrawnCards[i + 1] / 4;

                    form.Win.Add(new Type
                    {
                        Power = Power,
                        Current = -1
                    });

                    form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }

                if (form.DrawnCards[i] / 4 == 0 || form.DrawnCards[i + 1] / 4 == 0)
                {
                    current = -1;
                    Power = 13;

                    form.Win.Add(new Type
                    {
                        Power = Power,
                        Current = -1
                    });

                    form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
            }
        }

    }
}

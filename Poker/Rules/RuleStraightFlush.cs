using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Rules
{
    public class RuleStraightFlush
    {
        public static void FollowRule(ref double current, ref double power, int[] cardsOnTableWithPlayerCards, PokerForm form, int i)
        {
            var clubs = cardsOnTableWithPlayerCards.Where(o => o % 4 == 0).ToArray();
            var diamonds = cardsOnTableWithPlayerCards.Where(o => o % 4 == 1).ToArray();
            var hearts = cardsOnTableWithPlayerCards.Where(o => o % 4 == 2).ToArray();
            var spades = cardsOnTableWithPlayerCards.Where(o => o % 4 == 3).ToArray();

            var cardsOfClubs = clubs.Select(o => o / 4).Distinct().ToArray();
            var cardsOfDiamonds = diamonds.Select(o => o / 4).Distinct().ToArray();
            var cardsOfHearts = hearts.Select(o => o / 4).Distinct().ToArray();
            var cardsOfSpades = spades.Select(o => o / 4).Distinct().ToArray();

            if (current >= -1)
            {
                if (cardsOfClubs.Length >= 5)
                {
                    if (cardsOfClubs[0] + 4 == cardsOfClubs[4])
                    {
                        current = 8;
                        power = cardsOfClubs.Max() / 4 + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = power,
                            Current = 8
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (cardsOfClubs[0] == 0 && cardsOfClubs[1] == 9 && cardsOfClubs[2] == 10 && cardsOfClubs[3] == 11 && cardsOfClubs[0] + 12 == cardsOfClubs[4])
                    {
                        current = 9;
                        power = cardsOfClubs.Max() / 4 + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = power,
                            Current = 9
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (cardsOfDiamonds.Length >= 5)
                {
                    if (cardsOfDiamonds[0] + 4 == cardsOfDiamonds[4])
                    {
                        current = 8;
                        power = (cardsOfDiamonds.Max()) / 4 + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = power,
                            Current = 8
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (cardsOfDiamonds[0] == 0 && cardsOfDiamonds[1] == 9 && cardsOfDiamonds[2] == 10 && cardsOfDiamonds[3] == 11 && cardsOfDiamonds[0] + 12 == cardsOfDiamonds[4])
                    {
                        current = 9;
                        power = (cardsOfDiamonds.Max()) / 4 + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = power,
                            Current = 9
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (cardsOfHearts.Length >= 5)
                {
                    if (cardsOfHearts[0] + 4 == cardsOfHearts[4])
                    {
                        current = 8;
                        power = cardsOfHearts.Max() / 4 + (current * 100);
                        form.Win.Add(new Type
                        {
                            Power = power,
                            Current = 8
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (cardsOfHearts[0] == 0 && cardsOfHearts[1] == 9 && cardsOfHearts[2] == 10 && cardsOfHearts[3] == 11 && cardsOfHearts[0] + 12 == cardsOfHearts[4])
                    {
                        current = 9;
                        power = cardsOfHearts.Max() / 4 + (current * 100);
                        form.Win.Add(new Type
                        {
                            Power = power,
                            Current = 9
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (cardsOfSpades.Length >= 5)
                {
                    if (cardsOfSpades[0] + 4 == cardsOfSpades[4])
                    {
                        current = 8;
                        power = cardsOfSpades.Max() / 4 + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = power,
                            Current = 8
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (cardsOfSpades[0] == 0 && cardsOfSpades[1] == 9 && cardsOfSpades[2] == 10 && cardsOfSpades[3] == 11 && cardsOfSpades[0] + 12 == cardsOfSpades[4])
                    {
                        current = 9;
                        power = cardsOfSpades.Max() / 4 + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = power,
                            Current = 9
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

    }
}

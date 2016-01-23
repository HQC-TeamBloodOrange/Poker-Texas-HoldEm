using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Rules
{
    public class RuleFlush
    {
        public static void FollowRule(ref double current, ref double Power, ref bool vf, int[] Straight1, PokerForm form, int i)
        {
            if (current >= -1)
            {
                var f1 = Straight1.Where(o => o % 4 == 0).ToArray();
                var f2 = Straight1.Where(o => o % 4 == 1).ToArray();
                var f3 = Straight1.Where(o => o % 4 == 2).ToArray();
                var f4 = Straight1.Where(o => o % 4 == 3).ToArray();
                if (f1.Length == 3 || f1.Length == 4)
                {
                    if (form.DrawnCards[i] % 4 == form.DrawnCards[i + 1] % 4 && form.DrawnCards[i] % 4 == f1[0] % 4)
                    {
                        if (form.DrawnCards[i] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i] + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (form.DrawnCards[i + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i + 1] + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (form.DrawnCards[i] / 4 < f1.Max() / 4 && form.DrawnCards[i + 1] / 4 < f1.Max() / 4)
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f1.Length == 4) ////different cards in hand
                {
                    if (form.DrawnCards[i] % 4 != form.DrawnCards[i + 1] % 4 && form.DrawnCards[i] % 4 == f1[0] % 4)
                    {
                        if (form.DrawnCards[i] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i] + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (form.DrawnCards[i + 1] % 4 != form.DrawnCards[i] % 4 && form.DrawnCards[i + 1] % 4 == f1[0] % 4)
                    {
                        if (form.DrawnCards[i + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i + 1] + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f1.Length == 5)
                {
                    if (form.DrawnCards[i] % 4 == f1[0] % 4 && form.DrawnCards[i] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        Power = form.DrawnCards[i] + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (form.DrawnCards[i + 1] % 4 == f1[0] % 4 && form.DrawnCards[i + 1] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        Power = form.DrawnCards[i + 1] + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (form.DrawnCards[i] / 4 < f1.Min() / 4 && form.DrawnCards[i + 1] / 4 < f1.Min())
                    {
                        current = 5;
                        Power = f1.Max() + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f2.Length == 3 || f2.Length == 4)
                {
                    if (form.DrawnCards[i] % 4 == form.DrawnCards[i + 1] % 4 && form.DrawnCards[i] % 4 == f2[0] % 4)
                    {
                        if (form.DrawnCards[i] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i] + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (form.DrawnCards[i + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i + 1] + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (form.DrawnCards[i] / 4 < f2.Max() / 4 && form.DrawnCards[i + 1] / 4 < f2.Max() / 4)
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f2.Length == 4) ////different cards in hand
                {
                    if (form.DrawnCards[i] % 4 != form.DrawnCards[i + 1] % 4 && form.DrawnCards[i] % 4 == f2[0] % 4)
                    {
                        if (form.DrawnCards[i] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i] + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (form.DrawnCards[i + 1] % 4 != form.DrawnCards[i] % 4 && form.DrawnCards[i + 1] % 4 == f2[0] % 4)
                    {
                        if (form.DrawnCards[i + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i + 1] + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f2.Length == 5)
                {
                    if (form.DrawnCards[i] % 4 == f2[0] % 4 && form.DrawnCards[i] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        Power = form.DrawnCards[i] + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (form.DrawnCards[i + 1] % 4 == f2[0] % 4 && form.DrawnCards[i + 1] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        Power = form.DrawnCards[i + 1] + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (form.DrawnCards[i] / 4 < f2.Min() / 4 && form.DrawnCards[i + 1] / 4 < f2.Min())
                    {
                        current = 5;
                        Power = f2.Max() + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f3.Length == 3 || f3.Length == 4)
                {
                    if (form.DrawnCards[i] % 4 == form.DrawnCards[i + 1] % 4 && form.DrawnCards[i] % 4 == f3[0] % 4)
                    {
                        if (form.DrawnCards[i] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i] + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (form.DrawnCards[i + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i + 1] + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (form.DrawnCards[i] / 4 < f3.Max() / 4 && form.DrawnCards[i + 1] / 4 < f3.Max() / 4)
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f3.Length == 4) ////different cards in hand
                {
                    if (form.DrawnCards[i] % 4 != form.DrawnCards[i + 1] % 4 && form.DrawnCards[i] % 4 == f3[0] % 4)
                    {
                        if (form.DrawnCards[i] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i] + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (form.DrawnCards[i + 1] % 4 != form.DrawnCards[i] % 4 && form.DrawnCards[i + 1] % 4 == f3[0] % 4)
                    {
                        if (form.DrawnCards[i + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i + 1] + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f3.Length == 5)
                {
                    if (form.DrawnCards[i] % 4 == f3[0] % 4 && form.DrawnCards[i] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        Power = form.DrawnCards[i] + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (form.DrawnCards[i + 1] % 4 == f3[0] % 4 && form.DrawnCards[i + 1] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        Power = form.DrawnCards[i + 1] + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (form.DrawnCards[i] / 4 < f3.Min() / 4 && form.DrawnCards[i + 1] / 4 < f3.Min())
                    {
                        current = 5;
                        Power = f3.Max() + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f4.Length == 3 || f4.Length == 4)
                {
                    if (form.DrawnCards[i] % 4 == form.DrawnCards[i + 1] % 4 && form.DrawnCards[i] % 4 == f4[0] % 4)
                    {
                        if (form.DrawnCards[i] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i] + (current * 100);
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }

                        if (form.DrawnCards[i + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i + 1] + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (form.DrawnCards[i] / 4 < f4.Max() / 4 && form.DrawnCards[i + 1] / 4 < f4.Max() / 4)
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f4.Length == 4) ////different cards in hand
                {
                    if (form.DrawnCards[i] % 4 != form.DrawnCards[i + 1] % 4 && form.DrawnCards[i] % 4 == f4[0] % 4)
                    {
                        if (form.DrawnCards[i] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i] + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (form.DrawnCards[i + 1] % 4 != form.DrawnCards[i] % 4 && form.DrawnCards[i + 1] % 4 == f4[0] % 4)
                    {
                        if (form.DrawnCards[i + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i + 1] + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            form.Win.Add(new Type
                            {
                                Power = Power,
                                Current = 5
                            });
                            form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f4.Length == 5)
                {
                    if (form.DrawnCards[i] % 4 == f4[0] % 4 && form.DrawnCards[i] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        Power = form.DrawnCards[i] + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (form.DrawnCards[i + 1] % 4 == f4[0] % 4 && form.DrawnCards[i + 1] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        Power = form.DrawnCards[i + 1] + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (form.DrawnCards[i] / 4 < f4.Min() / 4 && form.DrawnCards[i + 1] / 4 < f4.Min())
                    {
                        current = 5;
                        Power = f4.Max() + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }
                ////ace
                if (f1.Length > 0)
                {
                    if (form.DrawnCards[i] / 4 == 0 && form.DrawnCards[i] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5.5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (form.DrawnCards[i + 1] / 4 == 0 && form.DrawnCards[i + 1] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5.5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f2.Length > 0)
                {
                    if (form.DrawnCards[i] / 4 == 0 && form.DrawnCards[i] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5.5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (form.DrawnCards[i + 1] / 4 == 0 && form.DrawnCards[i + 1] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5.5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f3.Length > 0)
                {
                    if (form.DrawnCards[i] / 4 == 0 && form.DrawnCards[i] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5.5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (form.DrawnCards[i + 1] / 4 == 0 && form.DrawnCards[i + 1] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5.5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f4.Length > 0)
                {
                    if (form.DrawnCards[i] / 4 == 0 && form.DrawnCards[i] % 4 == f4[0] % 4 && vf && f4.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5.5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (form.DrawnCards[i + 1] / 4 == 0 && form.DrawnCards[i + 1] % 4 == f4[0] % 4 && vf)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        form.Win.Add(new Type
                        {
                            Power = Power,
                            Current = 5.5
                        });
                        form.Sorted = form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

    }
}

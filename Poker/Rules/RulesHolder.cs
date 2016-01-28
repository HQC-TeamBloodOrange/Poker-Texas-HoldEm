namespace Poker.Rules
{
    using System.Linq;

    public class RulesHolder
    {
        public void FourOfAKind(double current, double Power, int[] Straight, PokerForm form, int i)
        {
            if (current >= -1)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (Straight[j] / 4 == Straight[j + 1] / 4 && Straight[j] / 4 == Straight[j + 2] / 4
                        && Straight[j] / 4 == Straight[j + 3] / 4)
                    {
                        current = 7;
                        Power = (Straight[j] / 4) * 4 + current * 100;
                        form.Win.Add(new Type { Power = Power, Current = 7 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0
                        && Straight[j + 3] / 4 == 0)
                    {
                        current = 7;
                        Power = 13 * 4 + current * 100;
                        form.Win.Add(new Type { Power = Power, Current = 7 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        public void FullHouse(double current, double Power, ref bool done, int[] Straight, PokerForm form, int i)
        {
            if (current >= -1)
            {
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
                                form.Win.Add(new Type { Power = Power, Current = 6 });
                                form.Sorted =
                                    form.Win.OrderByDescending(op1 => op1.Current)
                                        .ThenByDescending(op1 => op1.Power)
                                        .First();
                                break;
                            }
                            if (fh.Max() / 4 > 0)
                            {
                                current = 6;
                                Power = fh.Max() / 4 * 2 + current * 100;
                                form.Win.Add(new Type { Power = Power, Current = 6 });
                                form.Sorted =
                                    form.Win.OrderByDescending(op1 => op1.Current)
                                        .ThenByDescending(op1 => op1.Power)
                                        .First();
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
            }
        }

        public void PairFromHand(double current, double Power, PokerForm form, int i)
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

        public void PairTwoPair(double current, double Power, PokerForm form, int i)
        {
            if (current >= -1)
            {
                bool mesaggeBox = false;
                bool otherMesaggeBox = false;

                int maxIndexOfCardInTable = 16;
                int minIndexOfCardInTable = 12;

                for (int cardsInTableIndex = maxIndexOfCardInTable;
                     cardsInTableIndex >= minIndexOfCardInTable;
                     cardsInTableIndex--)
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
                                if (form.DrawnCards[cardsInTableIndex] / 4 != form.DrawnCards[i] / 4
                                    && form.DrawnCards[cardsInTableIndex] / 4 != form.DrawnCards[i + 1] / 4
                                    && current == 1)
                                {
                                    if (!mesaggeBox)
                                    {
                                        if (form.DrawnCards[i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (form.DrawnCards[i] / 4) * 2 + 13 * 4 + current * 100;
                                            form.Win.Add(new Type { Power = Power, Current = 2 });
                                            form.Sorted =
                                                form.Win.OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }

                                        if (form.DrawnCards[i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (form.DrawnCards[i + 1] / 4) * 2 + 13 * 4 + current * 100;
                                            form.Win.Add(new Type { Power = Power, Current = 2 });
                                            form.Sorted =
                                                form.Win.OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }

                                        if (form.DrawnCards[i + 1] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (form.DrawnCards[cardsInTableIndex] / 4) * 2
                                                    + (form.DrawnCards[i + 1] / 4) * 2 + current * 100;
                                            form.Win.Add(new Type { Power = Power, Current = 2 });
                                            form.Sorted =
                                                form.Win.OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }

                                        if (form.DrawnCards[i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (form.DrawnCards[cardsInTableIndex] / 4) * 2
                                                    + (form.DrawnCards[i] / 4) * 2 + current * 100;
                                            form.Win.Add(new Type { Power = Power, Current = 2 });
                                            form.Sorted =
                                                form.Win.OrderByDescending(op => op.Current)
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
                                                form.Win.Add(new Type { Power = Power, Current = 1 });
                                                form.Sorted =
                                                    form.Win.OrderByDescending(op => op.Current)
                                                        .ThenByDescending(op => op.Power)
                                                        .First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = form.DrawnCards[cardsInTableIndex] / 4 + form.DrawnCards[i] / 4
                                                        + current * 100;
                                                form.Win.Add(new Type { Power = Power, Current = 1 });
                                                form.Sorted =
                                                    form.Win.OrderByDescending(op => op.Current)
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
                                                form.Win.Add(new Type { Power = Power, Current = 1 });
                                                form.Sorted =
                                                    form.Win.OrderByDescending(op => op.Current)
                                                        .ThenByDescending(op => op.Power)
                                                        .First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = form.DrawnCards[cardsInTableIndex] / 4
                                                        + form.DrawnCards[i + 1] / 4 + current * 100;
                                                form.Win.Add(new Type { Power = Power, Current = 1 });

                                                form.Sorted =
                                                    form.Win.OrderByDescending(op => op.Current)
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

        public void Flush(double current, double Power, ref bool vf, int[] Straight1, PokerForm form, int i)
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
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        if (form.DrawnCards[i + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i + 1] + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else if (form.DrawnCards[i] / 4 < f1.Max() / 4 && form.DrawnCards[i + 1] / 4 < f1.Max() / 4)
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
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
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                    }
                    if (form.DrawnCards[i + 1] % 4 != form.DrawnCards[i] % 4 && form.DrawnCards[i + 1] % 4 == f1[0] % 4)
                    {
                        if (form.DrawnCards[i + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i + 1] + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
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
                        form.Win.Add(new Type { Power = Power, Current = 5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (form.DrawnCards[i + 1] % 4 == f1[0] % 4 && form.DrawnCards[i + 1] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        Power = form.DrawnCards[i + 1] + current * 100;
                        form.Win.Add(new Type { Power = Power, Current = 5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (form.DrawnCards[i] / 4 < f1.Min() / 4 && form.DrawnCards[i + 1] / 4 < f1.Min())
                    {
                        current = 5;
                        Power = f1.Max() + current * 100;
                        form.Win.Add(new Type { Power = Power, Current = 5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
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
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        if (form.DrawnCards[i + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i + 1] + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else if (form.DrawnCards[i] / 4 < f2.Max() / 4 && form.DrawnCards[i + 1] / 4 < f2.Max() / 4)
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
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
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                    }
                    if (form.DrawnCards[i + 1] % 4 != form.DrawnCards[i] % 4 && form.DrawnCards[i + 1] % 4 == f2[0] % 4)
                    {
                        if (form.DrawnCards[i + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i + 1] + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
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
                        form.Win.Add(new Type { Power = Power, Current = 5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (form.DrawnCards[i + 1] % 4 == f2[0] % 4 && form.DrawnCards[i + 1] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        Power = form.DrawnCards[i + 1] + current * 100;
                        form.Win.Add(new Type { Power = Power, Current = 5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (form.DrawnCards[i] / 4 < f2.Min() / 4 && form.DrawnCards[i + 1] / 4 < f2.Min())
                    {
                        current = 5;
                        Power = f2.Max() + current * 100;
                        form.Win.Add(new Type { Power = Power, Current = 5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
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
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        if (form.DrawnCards[i + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i + 1] + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else if (form.DrawnCards[i] / 4 < f3.Max() / 4 && form.DrawnCards[i + 1] / 4 < f3.Max() / 4)
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
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
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                    }
                    if (form.DrawnCards[i + 1] % 4 != form.DrawnCards[i] % 4 && form.DrawnCards[i + 1] % 4 == f3[0] % 4)
                    {
                        if (form.DrawnCards[i + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i + 1] + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
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
                        form.Win.Add(new Type { Power = Power, Current = 5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (form.DrawnCards[i + 1] % 4 == f3[0] % 4 && form.DrawnCards[i + 1] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        Power = form.DrawnCards[i + 1] + current * 100;
                        form.Win.Add(new Type { Power = Power, Current = 5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (form.DrawnCards[i] / 4 < f3.Min() / 4 && form.DrawnCards[i + 1] / 4 < f3.Min())
                    {
                        current = 5;
                        Power = f3.Max() + current * 100;
                        form.Win.Add(new Type { Power = Power, Current = 5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
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
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }

                        if (form.DrawnCards[i + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i + 1] + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else if (form.DrawnCards[i] / 4 < f4.Max() / 4 && form.DrawnCards[i + 1] / 4 < f4.Max() / 4)
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
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
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                    }
                    if (form.DrawnCards[i + 1] % 4 != form.DrawnCards[i] % 4 && form.DrawnCards[i + 1] % 4 == f4[0] % 4)
                    {
                        if (form.DrawnCards[i + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = form.DrawnCards[i + 1] + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 5 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
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
                        form.Win.Add(new Type { Power = Power, Current = 5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (form.DrawnCards[i + 1] % 4 == f4[0] % 4 && form.DrawnCards[i + 1] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        Power = form.DrawnCards[i + 1] + current * 100;
                        form.Win.Add(new Type { Power = Power, Current = 5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (form.DrawnCards[i] / 4 < f4.Min() / 4 && form.DrawnCards[i + 1] / 4 < f4.Min())
                    {
                        current = 5;
                        Power = f4.Max() + current * 100;
                        form.Win.Add(new Type { Power = Power, Current = 5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
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
                        form.Win.Add(new Type { Power = Power, Current = 5.5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (form.DrawnCards[i + 1] / 4 == 0 && form.DrawnCards[i + 1] % 4 == f1[0] % 4 && vf
                        && f1.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        form.Win.Add(new Type { Power = Power, Current = 5.5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f2.Length > 0)
                {
                    if (form.DrawnCards[i] / 4 == 0 && form.DrawnCards[i] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        form.Win.Add(new Type { Power = Power, Current = 5.5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (form.DrawnCards[i + 1] / 4 == 0 && form.DrawnCards[i + 1] % 4 == f2[0] % 4 && vf
                        && f2.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        form.Win.Add(new Type { Power = Power, Current = 5.5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f3.Length > 0)
                {
                    if (form.DrawnCards[i] / 4 == 0 && form.DrawnCards[i] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        form.Win.Add(new Type { Power = Power, Current = 5.5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (form.DrawnCards[i + 1] / 4 == 0 && form.DrawnCards[i + 1] % 4 == f3[0] % 4 && vf
                        && f3.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        form.Win.Add(new Type { Power = Power, Current = 5.5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f4.Length > 0)
                {
                    if (form.DrawnCards[i] / 4 == 0 && form.DrawnCards[i] % 4 == f4[0] % 4 && vf && f4.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        form.Win.Add(new Type { Power = Power, Current = 5.5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (form.DrawnCards[i + 1] / 4 == 0 && form.DrawnCards[i + 1] % 4 == f4[0] % 4 && vf)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        form.Win.Add(new Type { Power = Power, Current = 5.5 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        public void HighCard(double current, double Power, PokerForm form, int i)
        {
            if (current == -1)
            {
                if (form.DrawnCards[i] / 4 > form.DrawnCards[i + 1] / 4)
                {
                    current = -1;
                    Power = form.DrawnCards[i] / 4;

                    form.Win.Add(new Type { Power = Power, Current = -1 });
                    form.Sorted =
                        form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                else
                {
                    current = -1;
                    Power = form.DrawnCards[i + 1] / 4;

                    form.Win.Add(new Type { Power = Power, Current = -1 });

                    form.Sorted =
                        form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }

                if (form.DrawnCards[i] / 4 == 0 || form.DrawnCards[i + 1] / 4 == 0)
                {
                    current = -1;
                    Power = 13;

                    form.Win.Add(new Type { Power = Power, Current = -1 });

                    form.Sorted =
                        form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
            }
        }

        public void Straight(double current, double Power, int[] cardsOnTableWithPlayerCards, PokerForm form, int i)
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
                            form.Win.Add(new Type { Power = Power, Current = 4 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                        }
                        else
                        {
                            current = 4;
                            Power = op[j + 4] + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 4 });
                            form.Sorted =
                                form.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                        }
                    }
                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        current = 4;
                        Power = 13 + current * 100;
                        form.Win.Add(new Type { Power = Power, Current = 4 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        public void StraightFlush(
            double current,
            double power,
            int[] cardsOnTableWithPlayerCards,
            PokerForm form,
            int i)
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
                        form.Win.Add(new Type { Power = power, Current = 8 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (cardsOfClubs[0] == 0 && cardsOfClubs[1] == 9 && cardsOfClubs[2] == 10 && cardsOfClubs[3] == 11
                        && cardsOfClubs[0] + 12 == cardsOfClubs[4])
                    {
                        current = 9;
                        power = cardsOfClubs.Max() / 4 + current * 100;
                        form.Win.Add(new Type { Power = power, Current = 9 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (cardsOfDiamonds.Length >= 5)
                {
                    if (cardsOfDiamonds[0] + 4 == cardsOfDiamonds[4])
                    {
                        current = 8;
                        power = (cardsOfDiamonds.Max()) / 4 + current * 100;
                        form.Win.Add(new Type { Power = power, Current = 8 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (cardsOfDiamonds[0] == 0 && cardsOfDiamonds[1] == 9 && cardsOfDiamonds[2] == 10
                        && cardsOfDiamonds[3] == 11 && cardsOfDiamonds[0] + 12 == cardsOfDiamonds[4])
                    {
                        current = 9;
                        power = (cardsOfDiamonds.Max()) / 4 + current * 100;
                        form.Win.Add(new Type { Power = power, Current = 9 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (cardsOfHearts.Length >= 5)
                {
                    if (cardsOfHearts[0] + 4 == cardsOfHearts[4])
                    {
                        current = 8;
                        power = cardsOfHearts.Max() / 4 + (current * 100);
                        form.Win.Add(new Type { Power = power, Current = 8 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (cardsOfHearts[0] == 0 && cardsOfHearts[1] == 9 && cardsOfHearts[2] == 10
                        && cardsOfHearts[3] == 11 && cardsOfHearts[0] + 12 == cardsOfHearts[4])
                    {
                        current = 9;
                        power = cardsOfHearts.Max() / 4 + (current * 100);
                        form.Win.Add(new Type { Power = power, Current = 9 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (cardsOfSpades.Length >= 5)
                {
                    if (cardsOfSpades[0] + 4 == cardsOfSpades[4])
                    {
                        current = 8;
                        power = cardsOfSpades.Max() / 4 + current * 100;
                        form.Win.Add(new Type { Power = power, Current = 8 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (cardsOfSpades[0] == 0 && cardsOfSpades[1] == 9 && cardsOfSpades[2] == 10
                        && cardsOfSpades[3] == 11 && cardsOfSpades[0] + 12 == cardsOfSpades[4])
                    {
                        current = 9;
                        power = cardsOfSpades.Max() / 4 + current * 100;
                        form.Win.Add(new Type { Power = power, Current = 9 });
                        form.Sorted =
                            form.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        public void ThreeOfAKind(double current, double Power, int[] cardsOnTableWithPlayerCards, PokerForm form, int i)
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
                            form.Win.Add(new Type { Power = Power, Current = 3 });
                            form.Sorted =
                                form.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 3;
                            Power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + current * 100;
                            form.Win.Add(new Type { Power = Power, Current = 3 });
                            form.Sorted =
                                form.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                }
            }
        }

        public void TwoPairRule(double current, double Power, PokerForm form, int i)
        {
            if (current >= -1)
            {
                bool messageBox = false;

                int maxIndexOfCardInTable = 16;
                int minIndexOfCardInTable = 12;

                for (int cardInTableIndex = maxIndexOfCardInTable;
                     cardInTableIndex >= minIndexOfCardInTable;
                     cardInTableIndex--)
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
                                if (form.DrawnCards[i] / 4 == form.DrawnCards[cardInTableIndex] / 4
                                    && form.DrawnCards[i + 1] / 4 == form.DrawnCards[cardInTableIndex - k] / 4
                                    || form.DrawnCards[i + 1] / 4 == form.DrawnCards[cardInTableIndex] / 4
                                    && form.DrawnCards[i] / 4 == form.DrawnCards[cardInTableIndex - k] / 4)
                                {
                                    if (!messageBox)
                                    {
                                        if (form.DrawnCards[i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (form.DrawnCards[i + 1] / 4) * 2 + current * 100;
                                            form.Win.Add(new Type { Power = Power, Current = 2 });
                                            form.Sorted =
                                                form.Win.OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }

                                        if (form.DrawnCards[i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (form.DrawnCards[i] / 4) * 2 + current * 100;
                                            form.Win.Add(new Type { Power = Power, Current = 2 });
                                            form.Sorted =
                                                form.Win.OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }

                                        if (form.DrawnCards[i + 1] / 4 != 0 && form.DrawnCards[i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (form.DrawnCards[i] / 4) * 2 + (form.DrawnCards[i + 1] / 4) * 2
                                                    + current * 100;
                                            form.Win.Add(new Type { Power = Power, Current = 2 });
                                            form.Sorted =
                                                form.Win.OrderByDescending(op => op.Current)
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
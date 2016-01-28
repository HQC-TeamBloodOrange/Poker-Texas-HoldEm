namespace Poker.Win
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using Poker.Models;

    public class Winner
    {
        private int winnersCount;

        public void ShowWinner(
            double current,
            double power,
            AbstractPlayer player,
            Image[] deck,
            PictureBox[] holder,
            int chips,
            string lastly,
            List<AbstractPlayer> winners)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }

            for (var j = 0; j <= 16; j++)
            {
                if (holder[j].Visible)
                {
                    holder[j].Image = deck[j];
                }
            }

            if (current == player.PokerHandMultiplier) // (current == sorted.Current) 
            {
                if (power == player.Power)
                {
                    this.winnersCount++;
                    winners.Add(player);
                    if (current == -1)
                    {
                        MessageBox.Show(player + " High Card ");
                    }

                    if (current == 1 || current == 0)
                    {
                        MessageBox.Show(player + " Pair ");
                    }

                    if (current == 2)
                    {
                        MessageBox.Show(player + " Two Pair ");
                    }

                    if (current == 3)
                    {
                        MessageBox.Show(player + " Three of a Kind ");
                    }

                    if (current == 4)
                    {
                        MessageBox.Show(player + " Straight ");
                    }

                    if (current == 5 || current == 5.5)
                    {
                        MessageBox.Show(player + " Flush ");
                    }

                    if (current == 6)
                    {
                        MessageBox.Show(player + " Full House ");
                    }

                    if (current == 7)
                    {
                        MessageBox.Show(player + " Four of a Kind ");
                    }

                    if (current == 8)
                    {
                        MessageBox.Show(player + " Straight Flush ");
                    }

                    if (current == 9)
                    {
                        MessageBox.Show(player + " Royal Flush ! ");
                    }
                }
            }
        }

        public void SetWinnersChips(
            string lastly,
            List<AbstractPlayer> winners,
            TextBox textBoxChips,
            TextBox textBoxPot,
            int chips)
        {
            foreach (var winner in winners)
            {
                // lastfixed
                if (winner.Name == lastly)
                {
                    if (this.winnersCount > 1)
                    {
                        // winner += int.Parse(textBoxPot.Text) / this.winnersCount;
                        textBoxChips.Text = chips.ToString();
                    }

                    if (this.winnersCount == 1)
                    {
                        // winner += int.Parse(textBoxPot.Text);
                    }
                }
            }
        }
    }
}
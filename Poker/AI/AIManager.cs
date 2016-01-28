namespace Poker.AI
{
    using System.Windows.Forms;

    using Poker.Contracts;

    public class AIManager
    {
        public AIManager(CombinationManager manager)
        {
            this.Manager = manager;
        }

        public CombinationManager Manager { get; }

        /// <summary>
        /// Holds the logic for the win of the Bots.
        /// </summary>
        /// <param name="cardOne">Card One.</param>
        /// <param name="cardTwo">Card Two.</param>
        /// <param name="botPlayer">The bot player.</param>
        public void ArtificialIntelligence(
            int cardOne,
            int cardTwo,
            IPlayer botPlayer,
            PictureBox[] holder)
        {
            if (!botPlayer.FTurn)
            {
                if (botPlayer.PokerHandMultiplier == -1)
                {
                    this.Manager.HighCard(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 0)
                {
                    this.Manager.PairTable(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 1)
                {
                    this.Manager.PairHand(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 2)
                {
                    this.Manager.TwoPair(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 3)
                {
                    this.Manager.ThreeOfAKind(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 4)
                {
                    this.Manager.Straight(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 5 || botPlayer.PokerHandMultiplier == 5.5)
                {
                    this.Manager.Flush(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 6)
                {
                    this.Manager.FullHouse(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 7)
                {
                    this.Manager.FourOfAKind(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 8 || botPlayer.PokerHandMultiplier == 9)
                {
                    this.Manager.StraightFlush(botPlayer);
                }
            }

            if (botPlayer.FTurn)
            {
                holder[cardOne].Visible = false;
                holder[cardTwo].Visible = false;
            }
        }
    }
}
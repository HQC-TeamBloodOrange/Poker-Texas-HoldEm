﻿namespace Poker.AI
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
            //int cardOne, //int cardTwo, ////ref int botChips,//int botChips,//ref bool botTurn,//ref bool botPlayer.FTurn,
            //Label botStatus,//double botPower,//double botPlayer.PokerHandMultiplier)
            int cardOne,
            int cardTwo,
            IPlayer botPlayer,
            PictureBox[] holder)
        {
            // TODO: make it with switch case.
            if (!botPlayer.FTurn)
            {
                if (botPlayer.PokerHandMultiplier == -1)
                {
                    //HighCard(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, botPower);
                    this.Manager.HighCard(botPlayer);
                }
                //Switch bro
                if (botPlayer.PokerHandMultiplier == 0)
                {
                    //PairTable(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, botPower);
                    this.Manager.PairTable(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 1)
                {
                    //PairHand(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, botPower);
                    this.Manager.PairHand(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 2)
                {
                    //TwoPair(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, botPower);
                    this.Manager.TwoPair(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 3)
                {
                    //ThreeOfAKind(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, name, botPower);
                    this.Manager.ThreeOfAKind(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 4)
                {
                    //Straight(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, name, botPower);
                    this.Manager.Straight(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 5 || botPlayer.PokerHandMultiplier == 5.5)
                {
                    //Flush(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, name, botPower);
                    this.Manager.Flush(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 6)
                {
                    //FullHouse(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, name, botPower);
                    this.Manager.FullHouse(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 7)
                {
                    //FourOfAKind(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, name, botPower);
                    this.Manager.FourOfAKind(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 8 || botPlayer.PokerHandMultiplier == 9)
                {
                    //StraightFlush(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, name, botPower);
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
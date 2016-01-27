using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poker.Contracts;

namespace Poker.AI
{
    public class AI
    {
        // ----------------------------------------------------------------------
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
        IPlayer botPlayer, PictureBox[] holder
        )
        {
            CombinationManager combinationManager = new CombinationManager();

            // TODO: make it with switch case.
            if (!botPlayer.FTurn)
            {
                if (botPlayer.PokerHandMultiplier == -1)
                {
                    //HighCard(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, botPower);
                    combinationManager.HighCard(botPlayer);
                }
                //Switch bro
                if (botPlayer.PokerHandMultiplier == 0)
                {
                    //PairTable(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, botPower);
                    combinationManager.PairTable(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 1)
                {
                    //PairHand(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, botPower);
                    combinationManager.PairHand(botPlayer);

                }
                if (botPlayer.PokerHandMultiplier == 2)
                {
                    //TwoPair(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, botPower);
                    combinationManager.TwoPair(botPlayer);
                }
                if (botPlayer.PokerHandMultiplier == 3)
                {
                    //ThreeOfAKind(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, name, botPower);
                    combinationManager.ThreeOfAKind(botPlayer);

                }
                if (botPlayer.PokerHandMultiplier == 4)
                {
                    //Straight(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, name, botPower);
                    combinationManager.Straight(botPlayer);

                }
                if (botPlayer.PokerHandMultiplier == 5 || botPlayer.PokerHandMultiplier == 5.5)
                {
                    //Flush(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, name, botPower);
                    combinationManager.Flush(botPlayer);

                }
                if (botPlayer.PokerHandMultiplier == 6)
                {
                    //FullHouse(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, name, botPower);
                    combinationManager.FullHouse(botPlayer);

                }
                if (botPlayer.PokerHandMultiplier == 7)
                {
                    //FourOfAKind(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, name, botPower);
                    combinationManager.FourOfAKind(botPlayer);

                }
                if (botPlayer.PokerHandMultiplier == 8 || botPlayer.PokerHandMultiplier == 9)
                {
                    //StraightFlush(ref botChips, ref botTurn, ref botPlayer.FTurn, botStatus, name, botPower);
                    combinationManager.StraightFlush(botPlayer);

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

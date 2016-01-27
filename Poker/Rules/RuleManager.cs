using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Rules
{
    using Poker.Contracts;

    public class RuleManager
    {
        private const int CardsOnTable = 16;

        public static void FollowRules(
            int card1,
            int card2,
            //string currentText,
            //ref double current,
            //ref double Power,
            //bool foldedTurn,
            //double type,
            IPlayer botPlayer,
            PokerForm form)
        {

            #region Variables

            bool done = false;
            bool vf = false;
            var cardsOnTable = new int[5];
            var cardsOnTableWithPlayerCards = new int[7];

            cardsOnTableWithPlayerCards[0] = form.DrawnCards[card1];
            cardsOnTableWithPlayerCards[1] = form.DrawnCards[card2];

            cardsOnTable[0] = cardsOnTableWithPlayerCards[2] = form.DrawnCards[12];
            cardsOnTable[1] = cardsOnTableWithPlayerCards[3] = form.DrawnCards[13];
            cardsOnTable[2] = cardsOnTableWithPlayerCards[4] = form.DrawnCards[14];
            cardsOnTable[3] = cardsOnTableWithPlayerCards[5] = form.DrawnCards[15];
            cardsOnTable[4] = cardsOnTableWithPlayerCards[6] = form.DrawnCards[16];


            #endregion

            for (int i = 0; i < CardsOnTable; i++)
            {
                if (form.DrawnCards[i] == int.Parse(form.Holder[card1].Tag.ToString())
                    && form.DrawnCards[i + 1] == int.Parse(form.Holder[card2].Tag.ToString()))
                {
                    PairFromHand.FollowRule(botPlayer.PokerHandMultiplier, botPlayer.Power, form, i);
                    PairTwoPairRule.FollowRule(botPlayer.PokerHandMultiplier, botPlayer.Power, form, i);
                    RuleThreeOfAKind.FollowRule(botPlayer.PokerHandMultiplier, botPlayer.Power, cardsOnTableWithPlayerCards, form, i);
                    RuleStraight.FollowRule(botPlayer.PokerHandMultiplier, botPlayer.Power, cardsOnTableWithPlayerCards, form, i);
                    RuleFlush.FollowRule(botPlayer.PokerHandMultiplier, botPlayer.Power, ref vf, cardsOnTable, form, i);
                    FullHouse.FollowRule(botPlayer.PokerHandMultiplier, botPlayer.Power, ref done, cardsOnTable, botPlayer.Type, form, i);
                    FourOfAKind.FollowRule(botPlayer.PokerHandMultiplier, botPlayer.Power, cardsOnTable, form, i);
                    RuleStraightFlush.FollowRule(botPlayer.PokerHandMultiplier, botPlayer.Power, cardsOnTableWithPlayerCards, form, i);
                    RuleHighCard.FollowRule(botPlayer.PokerHandMultiplier, botPlayer.Power, form, i);
                }
            }
        }
    }
}

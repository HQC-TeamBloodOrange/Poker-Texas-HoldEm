using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Rules
{
    public class RuleManager
    {
        private const int CardsOnTable = 16;

        public static void FollowRules(
            int c1,
            int c2,
            string currentText,
            ref double current,
            ref double Power,
            bool foldedTurn,
            double type,
            PokerForm form)
        {

            #region Variables

            bool done = false;
            bool vf = false;
            var cardsOnTable = new int[5];
            var cardsOnTableWithPlayerCards = new int[7];

            cardsOnTableWithPlayerCards[0] = form.DrawnCards[c1];
            cardsOnTableWithPlayerCards[1] = form.DrawnCards[c2];

            cardsOnTable[0] = cardsOnTableWithPlayerCards[2] = form.DrawnCards[12];
            cardsOnTable[1] = cardsOnTableWithPlayerCards[3] = form.DrawnCards[13];
            cardsOnTable[2] = cardsOnTableWithPlayerCards[4] = form.DrawnCards[14];
            cardsOnTable[3] = cardsOnTableWithPlayerCards[5] = form.DrawnCards[15];
            cardsOnTable[4] = cardsOnTableWithPlayerCards[6] = form.DrawnCards[16];


            #endregion

            for (int i = 0; i < CardsOnTable; i++)
            {
                if (form.DrawnCards[i] == int.Parse(form.Holder[c1].Tag.ToString())
                    && form.DrawnCards[i + 1] == int.Parse(form.Holder[c2].Tag.ToString()))
                {
                    PairFromHand.FollowRule(ref current, ref Power, form, i);
                    PairTwoPairRule.FollowRule(ref current, ref Power, form, i);
                    RuleThreeOfAKind.FollowRule(ref current, ref Power, cardsOnTableWithPlayerCards, form, i);
                    RuleStraight.FollowRule(ref current, ref Power, cardsOnTableWithPlayerCards, form, i);
                    RuleFlush.FollowRule(ref current, ref Power, ref vf, cardsOnTable, form, i);
                    FullHouse.FollowRule(ref current, ref Power, ref done, cardsOnTable, type, form, i);
                    FourOfAKind.FollowRule(ref current, ref Power, cardsOnTable, form, i);
                    RuleStraightFlush.FollowRule(ref current, ref Power, cardsOnTableWithPlayerCards, form, i);
                    RuleHighCard.FollowRule(ref current, ref Power, form, i);
                }
            }
        }
    }
}

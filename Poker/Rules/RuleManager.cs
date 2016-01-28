namespace Poker.Rules
{
    using Poker.Contracts;

    public class RuleManager
    {
        private const int CardsOnTable = 16;

        private readonly RulesHolder rules = new RulesHolder();

        public void FollowRules(
            int card1,
            int card2,
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
                    this.rules.PairFromHand(botPlayer.PokerHandMultiplier, botPlayer.Power, form, i);
                    this.rules.PairTwoPair(botPlayer.PokerHandMultiplier, botPlayer.Power, form, i);
                    this.rules.ThreeOfAKind(
                        botPlayer.PokerHandMultiplier,
                        botPlayer.Power,
                        cardsOnTableWithPlayerCards,
                        form,
                        i);
                    this.rules.Straight(
                        botPlayer.PokerHandMultiplier,
                        botPlayer.Power,
                        cardsOnTableWithPlayerCards,
                        form,
                        i);
                    this.rules.Flush(botPlayer.PokerHandMultiplier, botPlayer.Power, ref vf, cardsOnTable, form, i);
                    this.rules.FullHouse(
                        botPlayer.PokerHandMultiplier,
                        botPlayer.Power,
                        ref done,
                        cardsOnTable,
                        form,
                        i);
                    this.rules.FourOfAKind(botPlayer.PokerHandMultiplier, botPlayer.Power, cardsOnTable, form, i);
                    this.rules.StraightFlush(
                        botPlayer.PokerHandMultiplier,
                        botPlayer.Power,
                        cardsOnTableWithPlayerCards,
                        form,
                        i);
                    this.rules.HighCard(botPlayer.PokerHandMultiplier, botPlayer.Power, form, i);
                    this.rules.TwoPairRule(botPlayer.PokerHandMultiplier, botPlayer.Power, form, i);
                }
            }
        }
    }
}
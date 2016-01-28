namespace Tests
{
    using System.Windows.Forms;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Poker;
    using Poker.Contracts;
    using Poker.Models;
    using Poker.Rules;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestHighCard_ShouldBeatLowerCard()
        {
            var highCard = 1;
            var lowerCard = 5;
            IPlayer bot1 = new Bot("Bot 1", new Label());
            IPlayer bot2 = new Bot("Bot 2", new Label());
            RulesHolder rules = new RulesHolder();
            rules.HighCard(bot1.PokerHandMultiplier, bot1.Power, new PokerForm(), 1);
        }
    }
}
namespace Poker.Core
{
    using Poker.Contracts;

    public class PokerDatabase
    {
        public PokerDatabase(PokerForm form)
        {
            //this.Players = form.Players;
        }

        public IPlayer[] Players { get; set; }
    }
}
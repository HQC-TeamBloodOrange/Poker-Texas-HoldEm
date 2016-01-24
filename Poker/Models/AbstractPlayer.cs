namespace Poker.Models
{
    using System.Windows.Forms;

    using Poker.Contracts;

    public class AbstractPlayer : IPlayer
    {
        private const int InitialChips = 10000;

        protected AbstractPlayer(string name, Label label)
        {
            this.CardsPanel = new Panel();
            this.GameEnded = false;
            this.Chips = InitialChips;
            this.Folded = false;
            this.Call = 0;
            this.Raise = 0;
            this.PokerHandMultiplier = -1;
            this.Name = name;
            this.Label = label;
        }

        public string Name { get; set; }

        public double Power { get; set; }

        public double Current { get; set; }

        public int Chips { get; set; }

        public bool Turn { get; set; }

        public bool GameEnded { get; set; }

        public string Status { get; set; }

        public double PokerHandMultiplier { get; set; }

        public double CardPower { get; set; }

        public bool Folded { get; set; }

        public int Call { get; set; }

        public int Raise { get; set; }

        public Panel CardsPanel { get; }

        public Label Label { get; set; }
    }
}

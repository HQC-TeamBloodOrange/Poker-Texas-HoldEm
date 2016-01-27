namespace Poker.Models
{
    using System.Windows.Forms;
    using Contracts;

    /// <summary>
    /// <see cref="AbstractPlayer"/> which holds properties about players.
    /// </summary>
    /// <seealso cref="Poker.Contracts.IPlayer" />
    public class AbstractPlayer : IPlayer
    {
        /// <summary>
        /// The initial chips
        /// </summary>
        private const int InitialChips = 10000;

        /// <summary>
        /// The label
        /// </summary>
        private Label label;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractPlayer"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="label">The label.</param>
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
            this.Type = -1;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the power.
        /// </summary>
        /// <value>
        /// The power.
        /// </value>
        public double Power { get; set; }

        /// <summary>
        /// Gets or sets the chips.
        /// </summary>
        /// <value>
        /// The chips.
        /// </value>
        public int Chips { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is <see cref="IPlayer" /> turn.
        /// </summary>
        /// <value>
        ///   <c>true</c> if turn; otherwise, <c>false</c>.
        /// </value>
        public bool Turn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is <see cref="IPlayer" /> FTurn.
        /// </summary>
        /// <value>
        ///   <c>true</c> if FTurn; otherwise, <c>false</c>.
        /// </value>
        public bool FTurn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the game has ended.
        /// </summary>
        /// <value>
        ///   <c>true</c> if game ended; otherwise, <c>false</c>.
        /// </value>
        public bool GameEnded { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the poker hand multiplier.
        /// </summary>
        /// <value>
        /// The poker hand multiplier.
        /// </value>
        public double PokerHandMultiplier { get; set; }

        /// <summary>
        /// Gets or sets the power of the card.
        /// </summary>
        /// <value>
        /// The power of the card.power of the card.
        /// </value>
        public double CardPower { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IPlayer" /> has folded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if folded; otherwise, <c>false</c>.
        /// </value>
        public bool Folded { get; set; }

        /// <summary>
        /// Gets or sets the call.
        /// </summary>
        /// <value>
        /// The call.
        /// </value>
        public int Call { get; set; }

        /// <summary>
        /// Gets or sets the raise.
        /// </summary>
        /// <value>
        /// The raise.
        /// </value>
        public int Raise { get; set; }

        /// <summary>
        /// Gets the cards panel.
        /// </summary>
        /// <value>
        /// The cards panel.
        /// </value>
        public Panel CardsPanel { get; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public Label Label
        {
            get
            {
                return this.label;
            }

            set
            {
                this.label = value;
            }
        }

        public double Type { get; set; }
    }
}

namespace Poker.Contracts
{
    using System.Windows.Forms;

    /// <summary>
    /// IPlayer interface holding properties.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the power.
        /// </summary>
        /// <value>
        /// The power.
        /// </value>
        double Power { get; set; }

        /// <summary>
        /// Gets or sets the chips.
        /// </summary>
        /// <value>
        /// The chips.
        /// </value>
        int Chips { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is <see cref="IPlayer"/> turn.
        /// </summary>
        /// <value>
        ///   <c>true</c> if turn; otherwise, <c>false</c>.
        /// </value>
        bool Turn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is <see cref="IPlayer"/> FTurn.
        /// </summary>
        /// <value>
        ///   <c>true</c> if FTurn; otherwise, <c>false</c>.
        /// </value>
        bool FTurn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the game has ended.
        /// </summary>
        /// <value>
        ///   <c>true</c> if game ended; otherwise, <c>false</c>.
        /// </value>
        bool GameEnded { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        string Status { get; set; }

        /// <summary>
        /// Gets or sets the poker hand multiplier.
        /// </summary>
        /// <value>
        /// The poker hand multiplier.
        /// </value>
        double PokerHandMultiplier { get; set; }

        /// <summary>
        /// Gets or sets the power of the card.
        /// </summary>
        /// <value>
        /// The power of the card.power of the card.
        /// </value>
        double CardPower { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IPlayer"/> has folded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if folded; otherwise, <c>false</c>.
        /// </value>
        bool Folded { get; set; }

        /// <summary>
        /// Gets or sets the call.
        /// </summary>
        /// <value>
        /// The call.
        /// </value>
        int Call { get; set; }

        /// <summary>
        /// Gets or sets the raise.
        /// </summary>
        /// <value>
        /// The raise.
        /// </value>
        int Raise { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        double Type { get; set; }

        /// <summary>
        /// Gets the cards panel.
        /// </summary>
        /// <value>
        /// The cards panel.
        /// </value>
        Panel CardsPanel { get; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        Label Label { get; set; }
    }
}

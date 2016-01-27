namespace Poker.Models
{
    using System.Windows.Forms;

    /// <summary>
    ///  Player class inherits the <see cref="AbstractPlayer"/> class and its members
    /// and sets the player's turn as true.
    /// </summary>
    /// <seealso cref="Poker.Models.AbstractPlayer" />
    public class Player : AbstractPlayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        public Player(Label label)
            : base("Player", label)
        {
            this.Turn = true;
        }
    }
}
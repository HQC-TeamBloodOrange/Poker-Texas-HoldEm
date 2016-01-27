namespace Poker.Models
{
    using System.Windows.Forms;

    /// <summary>
    /// Bot class inherits the <see cref="AbstractPlayer"/> class and its members.
    /// </summary>
    /// <seealso cref="Poker.Models.AbstractPlayer" />
    public class Bot : AbstractPlayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bot"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="label">The label.</param>
        public Bot(string name, Label label)
            : base(name, label)
        {
            this.Turn = false;
        }
    }
}

namespace Poker.Models
{
    using System.Windows.Forms;

    public class Player : AbstractPlayer
    {
        public Player(Label label)
            : base("Player", label)
        {
            this.Turn = true;
        }
    }
}

namespace Poker.Models
{
    using System.Windows.Forms;

    public class Player : AbstractPlayer
    {
        public Player(Label label)
            : base("player", label)
        {
            this.Turn = true;
        }
    }
}

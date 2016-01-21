namespace Poker.Models
{
    using System.Windows.Forms;

    public class Bot : AbstractPlayer
    {
        public Bot(string name, Label label)
            : base(name, label)
        {
            this.Turn = false;
        }
    }
}

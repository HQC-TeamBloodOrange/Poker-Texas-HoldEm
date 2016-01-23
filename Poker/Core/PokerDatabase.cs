using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

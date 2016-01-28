namespace Poker
{
    using System;

    using Poker.Contracts;

    public class CombinationManager
    {
        public CombinationManager(PokerForm form)
        {
            this.Form = form;
        }

        private PokerForm Form { get; }

        public void HighCard(IPlayer botPlayer)
        {
            this.Form.HP(botPlayer, 20, 25);
        }

        public void PairTable(IPlayer botPlayer)
        {
            this.Form.HP(botPlayer, 16, 25);
        }

        public void PairHand(IPlayer botPlayer)
        {
            var pairHand = new Random();
            var randomCall = pairHand.Next(10, 16);
            var randomRaise = pairHand.Next(10, 13);

            if (botPlayer.Power <= 199 && botPlayer.Power >= 140)
            {
                this.Form.PH(botPlayer, randomCall, 6, randomRaise);
            }

            if (botPlayer.Power <= 139 && botPlayer.Power >= 128)
            {
                this.Form.PH(botPlayer, randomCall, 7, randomRaise);
            }

            if (botPlayer.Power < 128 && botPlayer.Power >= 101)
            {
                this.Form.PH(botPlayer, randomCall, 9, randomRaise);
            }
        }

        public void TwoPair(IPlayer botPlayer)
        {
            var twoPair = new Random();
            var randomCall = twoPair.Next(6, 11);
            var randomRaise = twoPair.Next(6, 11);

            if (botPlayer.Power <= 290 && botPlayer.Power >= 246)
            {
                this.Form.PH(botPlayer, randomCall, 3, randomRaise);
            }

            if (botPlayer.Power <= 244 && botPlayer.Power >= 234)
            {
                this.Form.PH(botPlayer, randomCall, 4, randomRaise);
            }

            if (botPlayer.Power < 234 && botPlayer.Power >= 201)
            {
                this.Form.PH(botPlayer, randomCall, 4, randomRaise);
            }
        }

        public void ThreeOfAKind(IPlayer botPlayer)
        {
            var tk = new Random();
            var tCall = tk.Next(3, 7);
            var tRaise = tk.Next(4, 8);
            if (botPlayer.Power <= 390 && botPlayer.Power >= 330)
            {
                this.Form.Smooth(botPlayer, tCall, tRaise);
            }
            if (botPlayer.Power <= 327 && botPlayer.Power >= 321) 
            {
                this.Form.Smooth(botPlayer, tCall, tRaise);
            }
            if (botPlayer.Power < 321 && botPlayer.Power >= 303)
            {
                this.Form.Smooth(botPlayer, tCall, tRaise);
            }
        }

        public void Straight(IPlayer botPlayer)
        {
            var str = new Random();
            var sCall = str.Next(3, 6);
            var sRaise = str.Next(3, 8);
            if (botPlayer.Power <= 480 && botPlayer.Power >= 410)
            {
                this.Form.Smooth(botPlayer, sCall, sRaise);
            }
            if (botPlayer.Power <= 409 && botPlayer.Power >= 407)
            {
                this.Form.Smooth(botPlayer, sCall, sRaise);
            }
            if (botPlayer.Power < 407 && botPlayer.Power >= 404)
            {
                this.Form.Smooth(botPlayer, sCall, sRaise);
            }
        }

        public void Flush(IPlayer botPlayer)
        {
            var fsh = new Random();
            var fCall = fsh.Next(2, 6);
            var fRaise = fsh.Next(3, 7);
            this.Form.Smooth(botPlayer, fCall, fRaise);
        }

        public void FullHouse(IPlayer botPlayer)
        {
            var flh = new Random();
            var fhCall = flh.Next(1, 5);
            var fhRaise = flh.Next(2, 6);
            if (botPlayer.Power <= 626 && botPlayer.Power >= 620)
            {
                this.Form.Smooth(botPlayer, fhCall, fhRaise);
            }
            if (botPlayer.Power < 620 && botPlayer.Power >= 602)
            {
                this.Form.Smooth(botPlayer, fhCall, fhRaise);
            }
        }

        public void FourOfAKind(IPlayer botPlayer)
        {
            var fk = new Random();
            var fkCall = fk.Next(1, 4);
            var fkRaise = fk.Next(2, 5);
            if (botPlayer.Power <= 752 && botPlayer.Power >= 704)
            {
                this.Form.Smooth(botPlayer, fkCall, fkRaise);
            }
        }

        public void StraightFlush(IPlayer botPlayer)
        {
            var sf = new Random();
            var sfCall = sf.Next(1, 3);
            var sfRaise = sf.Next(1, 3);
            if (botPlayer.Power <= 913 && botPlayer.Power >= 804)
            {
                this.Form.Smooth(botPlayer, sfCall, sfRaise);
            }
        }
    }
}
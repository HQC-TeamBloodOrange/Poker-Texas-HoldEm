namespace Poker
{
    using System;
    using System.Windows.Forms;

    public class CombinationManager
    {
        public static void HighCard(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, double botPower)
        {
            //PokerForm.HP(ref botChips, ref botTurn, ref botFTurn, botStatus, botPower, 20, 25);
        }

        public static void PairTable(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, double botPower)
        {
            //PokerForm.HP(ref botChips, ref botTurn, ref botFTurn, botStatus, botPower, 16, 25);
        }

        public static void PairHand(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, double botPower)
        {
            var pairHand = new Random();
            var pairHandCall = pairHand.Next(10, 16);
            var pairHandRaise = pairHand.Next(10, 13);

            if (botPower <= 199 && botPower >= 140)
            {
                //PokerForm.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, pairHandCall, 6, pairHandRaise);
            }

            if (botPower <= 139 && botPower >= 128)
            {
               // PokerForm.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, pairHandCall, 7, pairHandRaise);
            }

            if (botPower < 128 && botPower >= 101)
            {
               // PokerForm.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, pairHandCall, 9, pairHandRaise);
            }
        }

        public static void TwoPair(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, double botPower)
        {
            var twoPair = new Random();
            var twoPairCall = twoPair.Next(6, 11);
            var twoPairRaise = twoPair.Next(6, 11);

            if (botPower <= 290 && botPower >= 246)
            {
               // PokerForm.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, twoPairCall, 3, twoPairRaise);
            }

            if (botPower <= 244 && botPower >= 234)
            {
              //  PokerForm.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, twoPairCall, 4, twoPairRaise);
            }

            if (botPower < 234 && botPower >= 201)
            {
               // PokerForm.PH(ref botChips, ref botTurn, ref botFTurn, botStatus, twoPairCall, 4, twoPairRaise);
            }
        }

        public static void ThreeOfAKind(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, int name, double botPower)
        {
            var threeOfKind = new Random();
            var threeOfKindCall = threeOfKind.Next(3, 7);
            var threeOfKindRaise = threeOfKind.Next(4, 8);
            if (botPower <= 390 && botPower >= 330)
            {
               // PokerForm.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, threeOfKindCall, threeOfKindRaise);
            }

            if (botPower <= 327 && botPower >= 321) //10  8
            {
               // PokerForm.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, threeOfKindCall, threeOfKindRaise);
            }

            if (botPower < 321 && botPower >= 303) //7 2
            {
               // PokerForm.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, threeOfKindCall, threeOfKindRaise);
            }
        }

        public static void Straight(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, int name, double botPower)
        {
            var straight = new Random();
            var straightCall = straight.Next(3, 6);
            var straightRaise = straight.Next(3, 8);
            if (botPower <= 480 && botPower >= 410)
            {
               // PokerForm.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, straightCall, straightRaise);
            }

            if (botPower <= 409 && botPower >= 407) //10  8
            {
              //  PokerForm.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, straightCall, straightRaise);
            }

            if (botPower < 407 && botPower >= 404)
            {
               // PokerForm.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, straightCall, straightRaise);
            }
        }

        public static void Flush(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, int name, double botPower)
        {
            var flush = new Random();
            var flushCall = flush.Next(2, 6);
            var flushRaise = flush.Next(3, 7);
            // PokerForm.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, flushCall, flushRaise);
        }

        public static void FullHouse(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, int name, double botPower)
        {
            var fullHouse = new Random();
            var fullHouseCall = fullHouse.Next(1, 5);
            var fullHouseRaise = fullHouse.Next(2, 6);
            if (botPower <= 626 && botPower >= 620)
            {
               // PokerForm.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, fullHouseCall, fullHouseRaise);
            }

            if (botPower < 620 && botPower >= 602)
            {
               // PokerForm.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, fullHouseCall, fullHouseRaise);
            }
        }

        public static void FourOfAKind(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, int name, double botPower)
        {
            var fourOfKind = new Random();
            var fourOfKindCall = fourOfKind.Next(1, 4);
            var fourOfKindRaise = fourOfKind.Next(2, 5);
            if (botPower <= 752 && botPower >= 704)
            {
               // PokerForm.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, fourOfKindCall, fourOfKindRaise);
            }
        }

        public static void StraightFlush(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, int name, double botPower)
        {
            var straightFlush = new Random();
            var straightFlushCall = straightFlush.Next(1, 3);
            var straightFlushRaise = straightFlush.Next(1, 3);
            if (botPower <= 913 && botPower >= 804)
            {
              //  PokerForm.Smooth(ref botChips, ref botTurn, ref botFTurn, botStatus, name, straightFlushCall, straightFlushRaise);
            }
        }
    }
}

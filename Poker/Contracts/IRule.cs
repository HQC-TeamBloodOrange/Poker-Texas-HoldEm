namespace Poker.Contracts
{
    public interface IRule
    {
        void FollowRule(double current, double Power, int[] Straight, PokerForm form, int i);

        void FollowRule(double current, double Power, PokerForm form, int i);

        void FollowRule(double current, double Power, ref bool done, int[] Straight, double type, PokerForm form, int i);

        void FollowRule(double current, double Power, ref bool vf, int[] Straight1, PokerForm form, int i);
    }
}
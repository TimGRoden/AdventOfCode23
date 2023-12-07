using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    public class Hand
    {
        public string hand { get; private set; }
        public int bid { get; private set; }
        static Dictionary<char,int> nameCards = new Dictionary<char, int>() { { 'A', 14 },{ 'K', 13 },{ 'Q', 12 },{ 'J', 11 },{ 'T', 10 } };
        public Hand(string line)
        {
            hand = line.Split(' ')[0];
            bid = int.Parse(line.Split(' ')[1]);
        }
        private int getCardVal(char c) => nameCards.Keys.Contains(c) ? nameCards[c] : int.Parse(c.ToString());
        public double getScore()
        {
            double scoremod = (double)getCardVal(hand[0]) / 12;
            char[] sorted = hand.ToArray();
            Array.Sort(sorted);
            if (sorted[0] == sorted.Last()) return 6 + scoremod; //5 of a kind.
            if (sorted[0] == sorted[3] || sorted[1] == sorted[4]) return 5 + scoremod; //4 of a kind
            if ((sorted[0] == sorted[2] && sorted[3] == sorted[4]) || (sorted[0] == sorted[1] && sorted[2] == sorted[4])) return 4 + scoremod;
            if ((sorted[0] == sorted[1] && sorted[1] == sorted[2]) ||
                (sorted[2] == sorted[1] && sorted[1] == sorted[3]) ||
                (sorted[2] == sorted[3] && sorted[4] == sorted[2])) return 3 + scoremod; //3 of a kind.
            int count = 0;
            for (int i = 0; i < 4; i++) if (sorted[i] == sorted[i+1]) count++;
            if (count == 2) return 2 + scoremod;
            if (count == 1) return 1 + scoremod;
            return scoremod;
        }
        public string getHand() => $"Hand: {hand}, Bid: {bid}, Score {getScore()}";
    }
}

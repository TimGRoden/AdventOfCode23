using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07v2
{
    public class Hand
    {
        public string hand { get; private set; }
        public int bid { get; private set; }
        static Dictionary<char, int> nameCards = new Dictionary<char, int>() { { 'A', 14 }, { 'K', 13 }, { 'Q', 12 }, { 'J', 11 }, { 'T', 10 } };
        public Hand(string line)
        {
            hand = line.Split(' ')[0];
            bid = int.Parse(line.Split(' ')[1]);
        }
        private int getCardVal(char c) => nameCards.Keys.Contains(c) ? nameCards[c] : int.Parse(c.ToString());
        public double getScore()
        {
            double[] Scores = new double[hand.Length+1];
            for (int i = 0; i < hand.Length; i++) Scores[i + 1] = getCardVal(hand[i]);
            Scores[0] = 0;
            char[] sorted = hand.ToArray();
            Array.Sort(sorted);
            if (sorted[0] == sorted.Last()) Scores[0] = 6; //5 of a kind.
            else if (sorted[0] == sorted[3] || sorted[1] == sorted[4]) Scores[0] = 5; //4 of a kind
            else if ((sorted[0] == sorted[2] && sorted[3] == sorted[4]) || (sorted[0] == sorted[1] && sorted[2] == sorted[4])) Scores[0] = 4;
            else if ((sorted[0] == sorted[1] && sorted[1] == sorted[2]) || (sorted[2] == sorted[1] && sorted[1] == sorted[3]) || (sorted[2] == sorted[3] && sorted[4] == sorted[2])) Scores[0] = 3; //3 of a kind.
            else {
                int count = 0;
                for (int i = 0; i < 4; i++) if (sorted[i] == sorted[i + 1]) count++;
                if (count == 2) Scores[0] = 2;
                else if (count == 1) Scores[0] = 1;
            }
            double total = 0;
            for (int i = 0; i < Scores.Length; i++)
            {
                total += Math.Pow(100, Scores.Length - i) * Scores[i];
            }
            return total;
        }
        public double getJokerScore()
        {
            double[] Scores = new double[hand.Length + 1];
            nameCards['J'] = 1;
            //Intention: Remove Jokers, then increase the handscore to represent this.
            //Calculate max = 5-CountJ
            //if countJ>3, return 5 of a kind. (4 jokers + card is 5 of a kind. CHECK JOKERS FIRST.
            //if COUNT(c)==max, '5 of a kind', use jokers to make it 5/5
            //if COUNT(c)==max-1, '4 of a kind', use jokers to make it 4/4
            //if COUNT(c)==max-2, 3 of a kind (check for pairs)
            //if (count(c)==max-3), pair (check for 2 of a kind)
            //if (count(c)==max-4), single card.


            nameCards['J'] = 11; //Reset to avoid editing for other parts.

            return 0;
        }
        public string getHand() => $"Hand: {hand}, Bid: {bid}, Score {getScore()}";
    }
}

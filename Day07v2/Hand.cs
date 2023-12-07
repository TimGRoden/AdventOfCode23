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

            return ScoreThis(Scores);
        }
        private int CountVal(char[] c, char x) => c.Aggregate(0, (n, l) => n + (l == x ? 1 : 0));
        public double getJokerScore()
        {
            double[] Scores = new double[hand.Length + 1];
            nameCards['J'] = 1;
            if (!hand.Contains("J")) return getScore(); //There's no jokers! Normal scoring applies.

            for (int i = 0; i < hand.Length; i++) Scores[i + 1] = getCardVal(hand[i]);
            string noJ = hand;
            while (noJ.Contains("J")) noJ = noJ.Replace("J", "");
            int countJ = hand.Length - noJ.Length;
            if (noJ.Length < 2) Scores[0] = 6; //Only 1 or 0 cards that aren't J. 4/5 wildcards = all same.
            else
            {
                char[] sorted = noJ.ToArray().OrderByDescending(x => CountVal(noJ.ToArray(), x)+(double)getCardVal(x)/100).ToArray(); //Deals with two pairs with the x/10 to organise them as pairs
                if (sorted[0] == sorted.Last()) Scores[0] = 6;
                else if (sorted[0] == sorted[sorted.Length - 2]) Scores[0] = 5;
                else if (noJ.Length == 3) Scores[0] = 3; //3 of a kind.
                else if (sorted[0] == sorted[1] && sorted[2] == sorted[3]) Scores[0] = 4; //Full house
                else if (sorted[0] == sorted[1]) Scores[0] = 3; //3oak
                else Scores[0] = 1; //pair
            }

            nameCards['J'] = 11; //Reset to avoid editing for other parts.
            return ScoreThis(Scores);
        }
        private double ScoreThis(double[] Scores)
        {
            double total = 0;
            for (int i = 0; i < Scores.Length; i++)
            {
                total += Math.Pow(100, Scores.Length - i-1) * Scores[i];
            }
            return total;
        }
        static Dictionary<int, string> result = new Dictionary<int, string>() {
{ 0, "High Card" }, {1, "Pair" }, {2, "2 Pairs" }, {3,"3 of a Kind" },{4,"Full House" },{5,"4 of a Kind" },{6,"5 of a Kind" } };
        private string convertHandType(double Score) => result[(int)Math.Floor(Score / 10000000000)];
        
        public string getHand(bool jokers=false) => $"Hand: {hand}, Bid: {bid}, Result: {convertHandType(jokers?getJokerScore():getScore())}";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    public class ScratchGame
    {
        private ScratchCard[] cards;
        private long[] quantity;
        public ScratchGame(ScratchCard[] cards)
        {
            this.cards = cards;
            quantity = new long[cards.Length];
            for (int i = 0; i < cards.Length; i++) quantity[i] = 1;
        }

        public long getMatches()
        {
            for (int i=0;i< cards.Length; i++)
            {
                //Console.WriteLine($"{quantity[i]} of Card {cards[i].ID}: {cards[i].getMatches()} matches.");
                for (int j = 1; j <= cards[i].getMatches(); j++)
                {
                    quantity[i + j] += quantity[i];
                }
            }
            return quantity.Sum();
        }
        public long getScore() => cards.Aggregate(0, (n, s) => n + s.getScore());
    }
}

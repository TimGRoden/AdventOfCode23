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
                for (int j = 0; j < cards[i].getMatches(); j++)
                {
                    quantity[i + j + 1] += quantity[i];
                }
            }
            return quantity.Sum();
        }
        public long getScore() => cards.Aggregate(0, (n, c) => n + c.getScore());
    }
}

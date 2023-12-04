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
        private bool visualise; private int delay;
        public ScratchGame(ScratchCard[] cards, bool visualise, int delay)
        {
            this.cards = cards; this.visualise = visualise; this.delay = delay;
            quantity = new long[cards.Length];
            for (int i = 0; i < cards.Length; i++) quantity[i] = 1;
        }
        private void VisualiseMatches(bool showScores = false)
        {
            if (Console.CursorTop + 3 > Console.WindowHeight) Console.Clear();
            foreach (ScratchCard card in cards) card.VisualiseMatches(showScores, delay);
        }
        public long getScore()
        {
            long result = cards.Aggregate(0, (n, c) => n + c.getScore());
            if (visualise) VisualiseMatches(true);
            return result;
        }
        public long getMatches()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (visualise)
                {
                    if (Console.CursorTop + 3 > Console.WindowHeight) Console.Clear();
                    Console.WriteLine($"{quantity[i]} of Card {cards[i].ID}: ");
                    cards[i].VisualiseMatches(true, delay);
                }
                int count = cards[i].getMatches();
                if (visualise && count != 0) Console.Write("Now there are: ");
                for (int j = 1; j <= count; j++)
                {
                    quantity[i + j] += quantity[i];
                    if (visualise) Console.Write($"{quantity[i+j]} of card {cards[i + j].ID}" + (j == count?".\n":", "));
                }
            }
            return quantity.Sum();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Day4
{
    public class ScratchCard
    {
        public List<int> winners { get; private set; }
        public List<int> numbers { get; private set; }
        public int ID { get; private set; }
        public ScratchCard(string game)
        {
            ID = int.Parse(game.Split(':')[0].Substring(5));
            string[] justVals = game.Split(':')[1].Split('|');
            winners = new List<int>(); numbers = new List<int>();
            foreach (string val in justVals[0].Split(' '))
            {
                if (val == "") continue;
                winners.Add(int.Parse(val));
            }
            foreach (string val in justVals[1].Split(' '))
            {
                if (val == "") continue;
                numbers.Add(int.Parse(val));
            }
        }
        public string getWinners() => winners.Aggregate("",(s,i) => s + i + ", ").Trim(", ".ToCharArray());
        public string getNumbers() => numbers.Aggregate("", (s, i) => s + i + ", ").Trim(", ".ToCharArray());
        public string getCard() => $"Card {ID}: {getWinners()} | {getNumbers()}";
        public void VisualiseMatches(bool showScores = false, int delay=100)
        {
            Console.Write(getCard());
            System.Threading.Thread.Sleep(delay);
            Console.CursorLeft = $"Card {ID}: {getWinners()} | ".Length;
            for (int i = 0; i < numbers.Count; i++)
            {
                if (winners.Contains(numbers[i])) Console.BackgroundColor = ConsoleColor.DarkMagenta;
                else Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(numbers[i]);
                Console.ResetColor();
                System.Threading.Thread.Sleep(delay);
                if (i < numbers.Count - 1) Console.Write(", ");
            }
            System.Threading.Thread.Sleep(delay);
            Console.Write($" => {getMatches()} matches");
            if (showScores)
            {
                System.Threading.Thread.Sleep(delay);
                Console.WriteLine($" => {getScore()} points");
            }
            else Console.WriteLine();
        }
        public int getScore() => (int)(0.5 * Math.Pow(2, getMatches()));
        public int getMatches()
        {
            int count = 0;
            foreach (int i in winners)
            {
                if (numbers.Contains(i)) count++;
            }
            return count;
        }
    }
}

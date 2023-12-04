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
        private List<int> winners, numbers;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    public class SubGame
    {
        public int[] storedBeads { get; private set; }
        public SubGame (string subGame)
        {
            storedBeads = new int[] { 0, 0, 0 };
            Queue<string> parts = new Queue<string>(subGame.Split(','));
            while (parts.Count > 0)
            {
                string beads = parts.Dequeue().Trim(' ');
                int count = int.Parse(beads.Split(' ')[0].Trim(' '));
                string bead = beads.Split(' ')[1];
                switch (bead[0])
                {
                    case 'r': storedBeads[0] += count; break;
                    case 'g': storedBeads[1] += count; break;
                    case 'b': storedBeads[2] += count; break;
                }
            }
        }
        public string printer()
        {
            string printer = "";
            if (storedBeads[0] != 0) printer += $"{storedBeads[0]} red, ";
            if (storedBeads[1] != 0) printer += $"{storedBeads[1]} green, ";
            if (storedBeads[2] != 0) printer += $"{storedBeads[2]} blue, ";
            return printer.Trim(", ".ToCharArray());
        }
        public bool isValid()
        {
            for (int i = 0; i < 3; i++) if (storedBeads[i] > i + 12) return false;
            return true;
        }
    }
}

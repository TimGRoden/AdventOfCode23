using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    public class PipeNode
    {
        public char type { get; private set; }
        public int xPos { get; private set; } public int yPos { get; private set; }
        public PipeNode nextNode, prevNode;
        public PipeNode(char t, int x, int y) => (type, xPos, yPos) = (t, x, y);
        public void printThisNode(int delay)
        {
            Console.SetCursorPosition(xPos, yPos);
            switch (type)
            {
                case '|': Console.Write('║'); break;
                case '-': Console.Write('═'); break;
                case 'F': Console.Write('╔'); break;
                case 'L': Console.Write('╚'); break;
                case '7': Console.Write('╗'); break;
                case 'J': Console.Write('╝'); break;
                default: Console.Write('▓'); break;
            }
            System.Threading.Thread.Sleep(delay);
        }
        public int getLoopLength(bool visualise = false, int delay = 100)
        {
            if (visualise)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                printThisNode(delay);
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            PipeNode curr = nextNode;
            int count = 1;
            while (curr != this)
            {
                if (visualise) curr.printThisNode(delay);
                curr = curr.nextNode;
                count++;
            }
            return count/2;
        }
        public List<Point> GetPointList()
        {
            List<Point> points = new List<Point>();
            PipeNode curr = this;
            do
            {
                Point current; current.x = curr.xPos; current.y = curr.yPos;
                points.Add(current);
                curr = curr.nextNode;
            } while (curr != this);
            return points;
        }

    }

}


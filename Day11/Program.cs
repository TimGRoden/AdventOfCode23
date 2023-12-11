using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day11
{
    internal class Program
    {
        static bool visualise = true;
        const int delay = 0;
        static ConsoleColor[] colors = { ConsoleColor.White, ConsoleColor.Gray, ConsoleColor.DarkGray};
        static char[] stars = { '■','#','o','S','0','┼','╬', '≈' };
        static Random rnd = new Random();
        static bool blankCol(string[] space, int col)
        {
            foreach (string s in space)
            {
                if (s[col] == '#') return false;
            }
            return true;
        }
        static long getDist(Point a, Point b, string[] space, int spacing)
        {
            long dist = Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);

            for (int row = Math.Min(a.y, b.y); row < Math.Max(a.y, b.y); row++)
            {
                if (!space[row].Contains('#')) dist += spacing;
            }
            for (int col = Math.Min(a.x,b.x); col < Math.Max(a.x,b.x); col++)
            {
                if (blankCol(space, col)) dist += spacing;
            }
            return dist;
        }
        public struct Point { public int x, y; }
        static List<Point> stripSpace(string[] contents)
        {
            List<Point> galaxies = new List<Point>();
            for (int row = 0; row< contents.Length; row++)
            {
                for (int col = 0; col < contents[0].Length; col++)
                {
                    if (contents[row][col] == '#')
                    {
                        Point g; g.x = col; g.y = row;
                        galaxies.Add(g);
                    }
                }
            }
            return galaxies;
        }
        static long TotalDists(string[] space, int spacing = 1)
        {
            List<Point> galaxies = stripSpace(space);
            long totalDist = 0;
            for (int i=0;i<galaxies.Count; i++)
            {
                for (int j = i+1; j<galaxies.Count; j++)
                {
                    totalDist += getDist(galaxies[i], galaxies[j], space, spacing);
                }
            }
            return totalDist;
        }
        static void PlotSpace(string[] space)
        {
            for (int row = 0; row< space.Length; row++)
            {
                if (!space[row].Contains('#'))
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(new string(' ', Math.Min(space[row].Length, Console.WindowWidth-1)));
                    Console.BackgroundColor = ConsoleColor.Black;
                    continue;
                }
                for (int col = 0; col < Math.Min(space[0].Length, Console.WindowWidth - 1); col++)
                {
                    if (blankCol(space, col))
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write(' ');
                        Console.BackgroundColor = ConsoleColor.Black;
                    } else
                    {
                        if (space[row][col] == '#')
                        {
                            Console.ForegroundColor = colors[rnd.Next(colors.Length)];
                            Console.Write(stars[rnd.Next(stars.Length)]);
                        }
                        else Console.Write(' ');
                    }
                    System.Threading.Thread.Sleep(delay);
                }
                Console.WriteLine();
                System.Threading.Thread.Sleep(delay);
            }

        }
        static void Main(string[] args)
        {
            string[] contents = File.ReadAllLines("input.txt");

            if (visualise)
            {
                Console.WriteLine("Press to start.");
                Console.ReadKey(true); Console.Clear();
                PlotSpace(contents);
            }

            long p1Tot = TotalDists(contents);
            Console.WriteLine($"Part 1: {p1Tot}");
            contents = File.ReadAllLines("input.txt");
            long p2Tot = TotalDists(contents,999999);
            Console.WriteLine($"Part 2: {p2Tot}");


            Console.ReadKey();
        }
    }
}

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
        static bool visualise = false;
        const int delay = 100;
        static void padAt(string[] space, int col)
        {
            for (int i = 0; i < space.Length; i++)
            {
                space[i] = space[i].Substring(0, col) + "." + space[i].Substring(col);
            }
        }
        static bool blankCol(string[] space, int col)
        {
            foreach (string s in space)
            {
                if (s[col] == '#') return false;
            }
            return true;
        }
        static long getDist(Point a, Point b, string[] space)
        {
            long dist = Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);

            for (int row = Math.Min(a.y, b.y); row < Math.Max(a.y, b.y); row++)
            {
                if (!space[row].Contains('#')) dist += 999999;
            }
            for (int col = Math.Min(a.x,b.x); col < Math.Max(a.x,b.x); col++)
            {
                if (blankCol(space, col)) dist += 999999;
            }
            return dist;
        }
        static string[] padSpace(string[] space)
        {
            for (int col = 0; col < space[0].Length; col++)
            {
                bool needsPadding = true;
                int row = 0;
                while (row < space.Length)
                {
                    if (space[row][col] == '#') { needsPadding = false; break; }
                    row++;
                }
                if (!needsPadding) continue;
                padAt(space, col);
                col++;
            }
            List<string> newSpace = new List<string>();
            for (int row = 0; row < space.Length; row++)
            {
                newSpace.Add(space[row]); //Always add once, should I add twice?
                if (!space[row].Contains('#')) newSpace.Add(space[row]);
            }
            return newSpace.ToArray();
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
        static long TotalDists(string[] space)
        {
            List<Point> galaxies = stripSpace(space);
            long totalDist = 0;
            for (int i=0;i<galaxies.Count; i++)
            {
                for (int j = i+1; j<galaxies.Count; j++)
                {
                    totalDist += getDist(galaxies[i], galaxies[j], space);
                }
            }
            return totalDist;
        }
        static void PlotSpace(List<Point> galaxies)
        {
            foreach (Point p in galaxies)
            {
                Console.SetCursorPosition(p.x, p.y);
                Console.Write("#");
                System.Threading.Thread.Sleep(delay);
            }
        }
        static int getDists(List<Point> galaxies)
        {
            int dist = 0;
            for (int i=0;i<galaxies.Count; i++)
            {
                for (int j=i+1;j<galaxies.Count; j++)
                { //Search every galaxy after this one.
                    dist += Math.Abs(galaxies[j].x - galaxies[i].x) + Math.Abs(galaxies[j].y - galaxies[i].y);
                }
            }
            return dist;
        }
        static void Main(string[] args)
        {
            string[] contents = padSpace(File.ReadAllLines("input.txt"));
            
            List<Point> galaxies = stripSpace(contents);
            int p1Tot = getDists(galaxies);
            if (visualise)
            {
                PlotSpace(galaxies);
                Console.SetCursorPosition(0, contents.Length);
            }
            Console.WriteLine($"Part 1: {p1Tot}");
            contents = File.ReadAllLines("input.txt");
            long p2Tot = TotalDists(contents);
            Console.WriteLine($"Part 2: {p2Tot}");


            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Day6
{
    internal class Program
    {
        static bool visualise = true;
        static int delay = 200;
        static List<long> striplongs(string s)
        {
            if (visualise)
            {
                Console.Write(s);
                System.Threading.Thread.Sleep(delay);
                Console.Write(" => [");
            }
            MatchCollection matches = Regex.Matches(s, @"[^\d][\d]+");
            List<long> result = new List<long>();
            foreach (Match match in matches)
            {
                if (visualise) Console.Write(match.Value.Trim(':') + ", ");
                result.Add(long.Parse(match.Value.Trim(':')));
            }
            if (!visualise) return result;

            Console.CursorLeft -= 2;
            Console.WriteLine("]");
            System.Threading.Thread.Sleep(delay);
            return result;
        }
        static Race[] getRaces(string[] contents)
        {
            List<long> times = striplongs(contents[0]);
            List<long> records = striplongs(contents[1]);
            Race[] races = new Race[times.Count];
            for (int i = 0; i < times.Count; i++)
            {
                if (visualise)
                {
                    Console.WriteLine($"Making race: {times[i]} ms, record: {records[i]}mm.");
                    System.Threading.Thread.Sleep(delay);
                }
                races[i] = new Race(times[i], records[i]);
            }

            return races;
        }
        static Race[] getBigRace(string[] contents)
        {
            contents[0] = contents[0].Replace(" ", "");
            contents[1] = contents[1].Replace(" ", "");
            return getRaces(contents);
        }
        static long Part1(Race[] races)
        {
            return races.Aggregate((long)1, (n, r) => n * r.winOpts(visualise));
        }
        static void Main(string[] args)
        {
            if (visualise)
            {
                Console.WriteLine("Press to begin.");
                Console.ReadKey(true); Console.Clear();
            }
            string[] contents = File.ReadAllLines("input.txt");
            Race[] races = getRaces(contents);
            Console.WriteLine($"Part 1: {Part1(races)}.");
            if (visualise)
            {
                Console.WriteLine("Press for Part 2.");
                Console.ReadKey(true); Console.Clear();
            }
            Console.WriteLine($"Part 2: {Part1(getBigRace(contents))}.");
            Console.ReadKey();
        }
    }
}

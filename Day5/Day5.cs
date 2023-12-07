using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Common;

namespace Day5
{
    internal class Day5
    {
        static bool visualise = true;
        static int delay = 100;

        static List<Map> maps = new List<Map>();
        static void getMaps(string[] contents)
        {
            List<string> mapContents = new List<string>();
            for (int i = 2; i < contents.Length; i++)
            {
                if (contents[i] == "")
                {
                    maps.Add(new Map(mapContents, visualise, delay));
                    mapContents = new List<string>();
                    continue;
                }
                mapContents.Add(contents[i]);
            }
            maps.Add(new Map(mapContents, visualise, delay));
            if (visualise)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                foreach (Map map in maps)
                {
                    if (Console.BackgroundColor == ConsoleColor.Black) Console.BackgroundColor = ConsoleColor.DarkGray;
                    else Console.BackgroundColor = ConsoleColor.Black; //Alternate colours
                    Console.WriteLine(map.getMap());
                }
                Console.ResetColor();
            }
        }
        static long getLocation(long seed)
        {
            if (visualise)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Seed {seed}");
                System.Threading.Thread.Sleep(delay);
            }
            string pos = "seed";
            long loc = seed;
            while (pos != "location")
            {
                foreach (Map map in maps)
                {
                    if (map.source == pos)
                    {
                        pos = map.destination;
                        loc = map.mapSeed(loc);
                        break;
                    }
                }
            }
            return loc;
        }
        static long getMinLocRange(long[] seeds)
        {
            string pos = "seed";
            List<long[]> locations = new List<long[]> { new long[] { seeds[0], seeds[1]} };

            while (pos != "location")
            {
                foreach (Map map in maps)
                {
                    if (map.source == pos)
                    {
                        pos = map.destination;
                        locations = map.mapRange(locations);
                        break;
                    }
                }
            }
            return locations.OrderBy(x => x[0]).ToArray()[0][0];
        }
        static void Main(string[] args)
        {
            string[] contents = File.ReadAllLines("input.txt");
            long[] seeds = contents[0].Substring(7).Split(' ').Select(x=>long.Parse(x)).ToArray();
            if (visualise)
            {
                Console.WriteLine("Maximise before pressing to continue.");
                Console.ReadKey(true);
                Console.Clear();
                Console.WriteLine($"Seeds: {seeds.Aggregate("",(l,s)=> l + s + ", ").Trim(", ".ToCharArray())}");
            }
            getMaps(contents);
            if (visualise)
            {
                Console.ResetColor();
                Console.WriteLine("Press to begin Part 1.");
                Console.ReadKey(true);
                Console.Clear();
            }
            Dictionary<long, long> Mapping = new Dictionary<long, long>();
            foreach (long seed in seeds) Mapping.Add(seed, getLocation(seed));
            long minLoc = Mapping.Values.OrderBy(x => x).ToArray()[0];
            if (visualise) Console.ResetColor();
            Console.WriteLine($"Part 1 Best Location {minLoc}.");
            if (visualise)
            {
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey(true);
                Console.Clear();
            }
            Mapping = new Dictionary<long, long>();
            for (int i = 0; i < seeds.Length; i += 2) {
                long[] seedrange = {  seeds[i], seeds[i + 1] };
                Mapping.Add(seeds[i], getMinLocRange(seedrange));
            }
            minLoc = Mapping.Values.OrderBy(x => x).ToArray()[0];
            Console.WriteLine($"Part 2 Best Location {minLoc}.");
            Console.ReadKey();
        }
    }
}

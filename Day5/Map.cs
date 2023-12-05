using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    public class Map
    {
        public string source { get; private set; } 
        public string destination {  get; private set; }
        private List<long[]> map;
        bool visualise; int delay;
        public Map(List<string> contents, bool visualise, int delay)
        {
            this.visualise = visualise; this.delay = delay;
            source = contents[0].Split('-')[0];
            destination = contents[0].Split('-')[2].Split(' ')[0].Trim(' ');
            map = new List<long[]>();
            for (int i=1;i<contents.Count;i++) map.Add(contents[i].Split(' ').Select(x => long.Parse(x.Trim())).ToArray());
        }
        public string getMap()
        {
            string result = $"{source}-to-{destination} map:\n";
            foreach (long[] mapping in map)
            {
                result += $"{mapping[1]} to {mapping[1] + mapping[2]-1} into {mapping[0]} to {mapping[0] + mapping[2]-1}\n";
            }
            return result;
        }

        public long mapSeed(long seed)
        {
            if (visualise) Console.Write($"{source} {seed}: mapped to ");
            foreach (long[] mapping in map)
            {
                if (seed < mapping[1] || seed > mapping[1] + mapping[2]) continue;
                //This is the right mapping.
                if (visualise)
                {
                    Console.WriteLine($"{destination} {mapping[0] + seed - mapping[1]}");
                    System.Threading.Thread.Sleep(delay);
                }
                return mapping[0] + seed - mapping[1];
            } //No mappings.
            if (visualise)
            {
                Console.WriteLine($"{destination} {seed}");
                System.Threading.Thread.Sleep(delay);
            }
            return seed;
        }
        static string printList(List<long[]> list)
        {
            string result = "[ ";
            foreach (long[] point in list) result += "[" + point[0] + ", " + point[1] + "], ";
            result = result.Substring(0,result.Length-2);
            return result + " ]";
        }
        private List<long[]> mapSeedRange(long[] seed)
        {
            if (visualise)
            {
                if (Console.CursorTop + 2 > Console.WindowHeight) Console.Clear();
                Console.WriteLine($"Mapping {seed[0]} to {seed[0] + seed[1]-1}");
                System.Threading.Thread.Sleep(delay);
            }
            List<long> positions = new List<long> { seed[0], seed[0] + seed[1] };
            foreach (long[] mapping in map)
            {
                if (mapping[1] > seed[0] && mapping[1] < seed[0]+seed[1]) positions.Add(mapping[1]);
                long maxmap = mapping[1] + mapping[2];
                if (maxmap > seed[0] && maxmap < seed[0] + seed[1]) positions.Add(maxmap+1);
            }
            positions = positions.OrderBy(x => x).ToList();
            List<long[]> result = new List<long[]>();
            for (int i = 0; i < positions.Count-1; i++)
            {
                if (positions[i + 1] - positions[i] < 0) continue;
                result.Add(new long[] { positions[i], positions[i + 1] - positions[i] });
            }
            for (int i = 0; i < result.Count; i++)
            {
                result[i][0] = mapSeed(result[i][0]);
            }
            if (visualise)
            {
                if (Console.CursorTop + 2 > Console.WindowHeight) Console.Clear();
                Console.WriteLine(printList(result));
                System.Threading.Thread.Sleep(delay);
            }
            return result;
        }

        public List<long[]> mapRange(List<long[]> seeds)
        {
            List<long[]> results = new List<long[]>();

            foreach (long[] seedset in seeds)
            { //Do each seed range individually.
                foreach (long[] res in mapSeedRange(seedset)) results.Add(res);
            }

            return results;
        }
    }
}

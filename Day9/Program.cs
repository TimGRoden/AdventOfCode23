using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Day9
{
    internal class Program
    {
        static bool visualise = false;
        const int delay = 100;
        static string printArr(int[] array) => $"[{array.Aggregate("", (l, i) => l + i + ", ").Trim(", ".ToCharArray())}]";
        static void Sleep() => System.Threading.Thread.Sleep(delay);
        static int getNext(int[] seq, bool first = false)
        {
            if (seq.All(x => x == 0))
            {
                if (visualise) Console.WriteLine();
                Sleep();
                return 0;
            }

            int[] diffs = new int[seq.Length - 1];
            for (int i = 1; i < seq.Length; i++) diffs[i - 1] = seq[i] - seq[i - 1];
            if (visualise) 
            { 
                Console.Write(" => " + printArr(diffs)); 
                Sleep(); 
            }
            if (first) return seq[0] - getNext(diffs, true);
            return seq.Last() + getNext(diffs);
        }
        static List<int[]> getSequences(string[] contents)
        {
            List<int[]> lines = new List<int[]>();
            string pattern = @"[-+]?\d+";
            foreach (string line in contents)
            {
                MatchCollection matches = Regex.Matches(line, pattern);
                int[] seq = new int[matches.Count];
                for (int i = 0; i < matches.Count; i++) seq[i] = int.Parse(matches[i].Value);
                lines.Add(seq);
            }
            return lines;
        }
        static int Part1(string[] contents)
        {
            List<int[]> lines = getSequences(contents);
            if (!visualise) return lines.Aggregate(0, (t, s) => t + getNext(s));

            int total = 0;
            foreach (int[] seq in lines)
            {
                Console.Write(printArr(seq));
                Sleep();
                total += getNext(seq);
            }
            return total;
        }
        static int Part2(string[] contents)
        {
            List<int[]> lines = getSequences(contents);
            if (!visualise) return lines.Aggregate(0, (t, s) => t + getNext(s, true));

            int total = 0;
            foreach (int[] seq in lines)
            {
                Console.Write(printArr(seq));
                Sleep();
                total += getNext(seq, true);
            }
            return total;
        }
        static void Main(string[] args)
        {
            string[] contents = File.ReadAllLines("input.txt");

            Console.WriteLine($"Part 1: {Part1(contents)}");
            Console.WriteLine($"Part 2: {Part2(contents)}");



            Console.ReadKey();
        }
    }
}

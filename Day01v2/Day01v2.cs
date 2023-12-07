using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
namespace Day01v2
{
    internal class Day01v2
    {
        static bool visualise = false;
        const int delay = 1000;
        static int Part1(string[] lines)
        {
            int total = 0;
            foreach (string line in lines)
            {
                List<string> parts = stripVals(line);
                total += int.Parse(parts[0] + parts.Last());
            }
            return total;
        }
        static string getListString(List<string> l) => "["+l.Aggregate("",(s,i)=>s+i+", ").Trim(", ".ToCharArray())+"]";
        static List<string> stripFirstLast(string line)
        {
            List<string> results = new List<string>();
            string forwardPattern = @"[\d]|one|two|three|four|five|six|seven|eight|nine";
            string backPattern = @"[\d]|"+ReverseS(forwardPattern.Substring(5));
            MatchCollection Matches = Regex.Matches(line, forwardPattern);
            results.Add(Matches[0].Value);
            Matches = Regex.Matches(ReverseS(line), backPattern);
            results.Add(ReverseS(Matches[0].Value));
            if (visualise) Console.WriteLine($"{line} => {results[0]}, {results[1]}");
            return results;
        }
        static string ReverseS(string s)
        {
            char[] chars = s.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
        static List<string> stripVals(string line, bool part2 = false)
        {
            string pattern = part2 ? @"[\d]|one|two|three|four|five|six|seven|eight|nine" : @"[\d]";
            List<string> results = new List<string>();
            foreach (Match match in Regex.Matches(line, pattern)) results.Add(match.Value);
            while (results.Contains("")) results.Remove("");
            if (visualise) Console.WriteLine($"{line} => {getListString(results)}");
            return results;
        }
        static Dictionary<string, string> converter = new Dictionary<string, string>() {    { "one", "1" },
                                                                                            { "two", "2" },
                                                                                            { "three", "3" },
                                                                                            { "four", "4" },
                                                                                            { "five", "5" },
                                                                                            { "six", "6" },
                                                                                            { "seven", "7" },
                                                                                            { "eight", "8" },
                                                                                            { "nine", "9" }};
        static string getVal(string item)
        {
            if (converter.Keys.Contains(item)) return converter[item];
            return item;
        }
        static int Part2(string[] lines)
        {
            int total = 0;
            foreach (string line in lines)
            {
                List<string> parts = stripFirstLast(line);
                string p1 = getVal(parts[0]); string p2 = getVal(parts.Last());
                total += int.Parse(p1+p2);
            }
            return total;

        }
        static void Main(string[] args)
        {
            string[] contents = File.ReadAllLines("input.txt");
            if (visualise)
            {
                Console.WriteLine("Press to continue.");
                Console.ReadKey(true); Console.Clear();
            }
            Console.WriteLine($"Part 1: {Part1(contents)}.");
            if (visualise)
            {
                Console.WriteLine("Press to continue.");
                Console.ReadKey(true); Console.Clear();
            }
            Console.WriteLine($"Part 2: {Part2(contents)}.");

            Console.ReadKey();
            //MatchCollection matches = Regex.Matches(s, @"[^\d][\d]+");
            //List<long> result = new List<long>();
            //foreach (Match match in matches)
            //{
            //    if (visualise) Console.Write(match.Value.Trim(':') + ", ");
            //    result.Add(long.Parse(match.Value.Trim(':')));
            //}
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace Day1
{
    internal class Day1
    {
        static void printList(List<string> items) => Console.WriteLine(items.Aggregate("[", (r, i) => i + ", ").Trim(", ".ToCharArray()).ToString() + "]");
        static List<string> stripDown(string line, bool part2 = false)
        {
            List<string> result = new List<string>();
            if (!part2)
            {
                foreach (char c in line) if (char.IsNumber(c)) result.Add(c.ToString());
            }
            else
            {
                Dictionary<int,string> foundItems = new Dictionary<int,string>();
                foreach (string word in valids)
                {
                    if (line.Contains(word))
                    {
                        //Find all instances of the word.
                        for (int i = 0; i <= line.Length - word.Length; i++)
                        {
                            if (line.Substring(i, word.Length) == word) foundItems.Add(i, word);
                        }
                    }
                }
                foreach (int key in foundItems.Keys.OrderBy(x => x).ToArray()) result.Add(foundItems[key]);
            }
            return result;
        }
        static int getResult(List<string> parts)
        {
            string result = "";
            //Console.Write(printList(parts));
            if (words.ContainsKey(parts[0])) result += words[parts[0]];
            else result += parts[0];
            parts.Reverse();
            if (words.ContainsKey(parts[0])) result += words[parts[0]];
            else result += parts[0];
            //Console.WriteLine(" => "+result);
            return int.Parse(result);
        }
        static Dictionary<string, int> words = new Dictionary<string, int>(){
            { "one",    1 },
            { "two",    2 },
            { "three",  3 },
            { "four",   4 },
            { "five",   5 },
            { "six",    6 },
            { "seven",  7 },
            { "eight",  8 },
            { "nine",   9 } };
        static string[] valids = { "one", "1",  "two",    "2",  "three",  "3" ,"four",   "4" , "five",   "5" , 
            "six",    "6" , "seven",  "7" , "eight",  "8" , "nine",   "9" };
        static void Main(string[] args)
        {
            string[] contents = File.ReadAllLines("input.txt");
            foreach (string line in contents) stripDown(line, true);
            int total = 0;
            foreach (string line in contents) total += getResult(stripDown(line));
            Console.WriteLine($"Part 1: {total}");
            total = 0;
            foreach (string line in contents) total += getResult(stripDown(line, true));
            Console.WriteLine($"Part 2: {total}");
            Console.ReadKey();
        }
    }
}

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
        static bool visualise = true;
        static int delay = 10;
        static void printList(List<string> items) => Console.WriteLine(items.Aggregate("[", (r, i) => i + ", ").Trim(", ".ToCharArray()).ToString() + "]");
        static List<string> stripDown(string line, bool part2 = false)
        {
            if (visualise)
            {
                if (Console.CursorTop >= Console.WindowHeight - 2) Console.Clear();
                Console.Write(line);
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                System.Threading.Thread.Sleep(delay);
            }
            List<string> result = new List<string>();
            if (!part2)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (char.IsNumber(line[i]))
                    {
                        result.Add(line[i].ToString());
                        if (visualise)
                        {
                            Console.CursorLeft = i;
                            Console.Write(line[i]);
                            System.Threading.Thread.Sleep(delay);
                            
                        }
                    }
                }
            }
            else
            {
                Dictionary<int,string> foundItems = new Dictionary<int,string>();
                foreach (string word in valids)
                {
                    if (!line.Contains(word)) continue;
                        //Find all instances of the word.
                    for (int i = 0; i <= line.Length - word.Length; i++)
                    {
                        if (line.Substring(i, word.Length) != word) continue;
                        foundItems.Add(i, word);
                        if (visualise)
                        {
                            Console.CursorLeft = i;
                            if (word.Length == 1) Console.BackgroundColor = ConsoleColor.DarkMagenta;
                            else Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.Write(word);
                            System.Threading.Thread.Sleep(delay);
                        }
                    }
                    
                }
                foreach (int key in foundItems.Keys.OrderBy(x => x).ToArray()) result.Add(foundItems[key]);
            }
            if (visualise)
            {
                Console.ResetColor();
                Console.CursorLeft = line.Length + 1;
                Console.Write($"=> {getResult(result)}");
                Console.WriteLine();   
            }
            return result;
        }
        static int getResult(List<string> parts)
        {
            string result = "";
            if (words.ContainsKey(parts[0])) result += words[parts[0]];
            else result += parts[0];
            parts.Reverse();
            if (words.ContainsKey(parts[0])) result += words[parts[0]];
            else result += parts[0];
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
            if (visualise)
            {
                Console.WriteLine("Press any key to visualise:");
                Console.ReadKey(true);
                Console.Clear();
            }
            string[] contents = File.ReadAllLines("input.txt");
            Console.WriteLine($"Part 1: {contents.Aggregate(0, (t, l) => t + getResult(stripDown(l)))}");
            Console.WriteLine($"Part 2: {contents.Aggregate(0,(t,l) => t + getResult(stripDown(l, true)))}");
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day07v2
{
    internal class Program
    {
        private static bool visualise = true;

        private const int delay = 500;

        private static long Part1(string[] contents)
        {
            Hand[] hands = new Hand[contents.Length];
            for (int i = 0; i < contents.Length; i++)
            {
                hands[i] = new Hand(contents[i]);
            }
            if (visualise)
            {
                foreach (Hand hand in hands) Console.WriteLine(hand.getHand().Substring(0,hand.getHand().IndexOf('R')-2));
                System.Threading.Thread.Sleep(delay);
            }
            Hand[] sortedHands = hands.OrderBy((Hand x) => x.getScore()).ToArray();
            if (visualise)
            {
                Console.WriteLine($"\nHands in score-order:");
                for (int i=0;i<sortedHands.Length;i++)
                {
                    Console.WriteLine($"{i+1}. "+ sortedHands[i].getHand());
                    Console.WriteLine($"{new string(' ',("  "+(i+1)).Length)}{i + 1} x {sortedHands[i].bid} = {(i + 1) * sortedHands[i].bid}");
                    System.Threading.Thread.Sleep(delay);
                }
            }
            long num = 0;
            for (int i = 0; i < sortedHands.Length; i++) num += (i + 1) * sortedHands[i].bid;
            return num;
        }
        private static long Part2(string[] contents)
        {
            Hand[] hands = new Hand[contents.Length];
            for (int i = 0; i < contents.Length; i++)
            {
                hands[i] = new Hand(contents[i]);
            }
            if (visualise)
            {
                foreach (Hand hand in hands) Console.WriteLine(hand.getHand().Substring(0, hand.getHand().IndexOf('R') - 2));
                System.Threading.Thread.Sleep(delay);
            }
            Hand[] sortedHands = hands.OrderBy((Hand x) => x.getJokerScore()).ToArray();
            if (visualise)
            {
                Console.WriteLine($"\nHands in score-order:");
                for (int i = 0; i < sortedHands.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. " + sortedHands[i].getHand(true)); 
                    Console.WriteLine($"{new string(' ', ("  " + (i + 1)).Length)}{i + 1} x {sortedHands[i].bid} = {(i + 1) * sortedHands[i].bid}");
                    System.Threading.Thread.Sleep(delay);
                }
            }
            long num = 0;
            for (int i = 0; i < sortedHands.Length; i++) num += (i + 1) * sortedHands[i].bid;
            return num;
        }
        private static void Main(string[] args)
        {
            string[] contents = File.ReadAllLines("input.txt");
            if (visualise)
            {
                Console.WriteLine("Press to begin.");
                Console.ReadKey(true); Console.Clear();
            }
            Console.WriteLine($"Part 1: {Part1(contents)}.");
            if (visualise)
            {
                Console.WriteLine("Press to begin part 2.");
                Console.ReadKey(true); Console.Clear();
            }
            Console.WriteLine($"Part 2: {Part2(contents)}.");
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day7
{
    internal class Program
    {
        static bool visualise = true;
        const int delay = 100;
        static long Part1(string[] contents)
        {
            Hand[] hands = new Hand[contents.Length];
            for (int i=0;i<contents.Length; i++) hands[i] = new Hand(contents[i]);
            if (visualise) foreach (Hand hand in hands) Console.WriteLine(hand.getHand());
            Hand[] sortedHands = hands.OrderBy(x => x.getScore()).ToArray();
            if (visualise) foreach (Hand hand in sortedHands) Console.WriteLine(hand.getHand());
            long total = 0;
            for (int i = 0; i < sortedHands.Length; i++)
            {
                if (visualise)
                {
                    Console.WriteLine($"{i+1} x {sortedHands[i].bid} = {(i + 1) * sortedHands[i].bid}");
                }
                total += (i + 1) * sortedHands[i].bid;
            }
            return total;
        }
        static void Main(string[] args)
        {
            string[] contents = File.ReadAllLines("input.txt");

            Console.WriteLine($"Part 1: {Part1(contents)}.");




            Console.ReadKey();
        }
    }
}

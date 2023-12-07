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
        private static bool visualise = false;

        private const int delay = 100;

        private static long Part1(string[] contents)
        {
            Hand[] array = new Hand[contents.Length];
            for (int i = 0; i < contents.Length; i++)
            {
                array[i] = new Hand(contents[i]);
            }
            if (visualise)
            {
                Hand[] array2 = array;
                foreach (Hand hand in array2)
                {
                    Console.WriteLine(hand.getHand());
                }
            }
            Hand[] array3 = array.OrderBy((Hand x) => x.getScore()).ToArray();
            if (visualise)
            {
                Hand[] array4 = array3;
                foreach (Hand hand2 in array4)
                {
                    Console.WriteLine(hand2.getHand());
                }
            }
            long num = 0L;
            for (int l = 0; l < array3.Length; l++)
            {
                if (visualise)
                {
                    Console.WriteLine($"{l + 1} x {array3[l].bid} = {(l + 1) * array3[l].bid}");
                }
                num += (l + 1) * array3[l].bid;
            }
            return num;
        }

        private static void Main(string[] args)
        {
            string[] contents = File.ReadAllLines("input.txt");
            Console.WriteLine($"Part 1: {Part1(contents)}.");
            Console.ReadKey();
        }
    }
}

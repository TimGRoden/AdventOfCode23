using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Day4
{
    internal class Day4
    {
        static bool visualise = true;
        const int delay = 100;
        static ScratchCard[] getGames(string[] contents)
        {
            ScratchCard[] games = new ScratchCard[contents.Length];
            for (int i = 0; i < contents.Length; i++) games[i] = new ScratchCard(contents[i]);
            return games;
        }
        static void Main(string[] args)
        {
            string[] contents = File.ReadAllLines("input.txt");
            if (visualise)
            {
                Console.WriteLine("Please maximise and press any key to begin.");
                Console.ReadKey(true);
                Console.Clear();
            }
            ScratchGame wholeGame = new ScratchGame(getGames(contents), visualise, delay);
            Console.WriteLine($"Part 1: {wholeGame.getScore()}.");
            if (visualise)
            {
                Console.ReadKey(true);
                Console.Clear();
            }
            Console.WriteLine($"Part 2: {wholeGame.getMatches()}.");
            Console.ReadKey();
        }
    }
}

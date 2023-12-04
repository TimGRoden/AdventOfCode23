using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Day4
{
    internal class Program
    {
        static ScratchCard[] getGames(string[] contents)
        {
            ScratchCard[] games = new ScratchCard[contents.Length];
            for (int i = 0; i < contents.Length; i++) games[i] = new ScratchCard(contents[i]);
            return games;
        }
        static void Main(string[] args)
        {
            ScratchGame game = new ScratchGame(getGames(File.ReadAllLines("input.txt")));
            Console.WriteLine($"Part 1: {game.getScore()}.");
            Console.WriteLine($"Part 2: {game.getMatches()}.");
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.CodeDom.Compiler;

namespace Day2
{
    internal class Day2
    {
        static int getID(string game)
        {
            string[] parts = game.Split(':');
            return int.Parse(parts[0].Substring(5));
        }
        static Dictionary<char, int> maxBeads = new Dictionary<char, int>() { {'r',12 }, {'g',13 }, {'b',14 } };
        static bool isValidSubgame(string subGame)
        { // 3 blue, 4 red;
            // 2 green
            foreach (string beads in subGame.Split(','))
            {
                string bead = beads.Trim(' ');
                int count = int.Parse(bead.Split(' ')[0]);
                if (maxBeads[bead.Split(' ')[1][0]] < count) return false;
            }
            return true;
        }
        static bool isValid(string game)
        {
            string subGames = game.Split(':')[1];
            Queue<string> gameBeads = new Queue<string>(subGames.Split(';'));
            while (gameBeads.Count > 0)
            {
                if (!isValidSubgame(gameBeads.Dequeue())) return false;
            }
            return true;
        }
        static int[] countBeads(string game)
        {
            int[] result = { 0, 0, 0 }; //RGB
            Queue<string> parts = new Queue<string>(game.Split(','));
            while (parts.Count > 0)
            {
                string beads = parts.Dequeue().Trim(' ');
                int count = int.Parse(beads.Split(' ')[0].Trim(' '));
                string bead = beads.Split(' ')[1];
                switch (bead[0])
                {
                    case 'r': result[0]+=count; break;
                    case 'g': result[1]+=count; break;
                    case 'b': result[2]+=count; break;
                }
            }
            return result;
        }
        static int[] minBeads(string game)
        {
            int[] result = new int[] { 0, 0, 0 };
            Queue<string> subgame = new Queue<string>(game.Split(':')[1].Split(';'));
            while (subgame.Count > 0)
            {
                int[] needed = countBeads(subgame.Dequeue());
                for (int i=0;i<needed.Length; i++) if (needed[i] > result[i]) result[i] = needed[i];
            }
            //Console.WriteLine(game);
            //Console.Write($"MIN: {result[0]} red, {result[1]} green, {result[2]} blue. ");
            return result;
        }
        static int powerBeads(string game) => minBeads(game).Aggregate(1, (n, b) => n * b);
        static void Main(string[] args)
        {
            string[] contents = File.ReadAllLines("input.txt");
            int total = 0;
            foreach (string game in contents)
            {
                if (isValid(game)) total += getID(game);
            }
            Console.WriteLine($"Part1: {total}.");
            long powerTotal = 0;
            foreach (string game in contents) powerTotal += powerBeads(game);
            Console.WriteLine($"Part2: {powerTotal}.");
            Console.ReadKey();

        }
    }
}

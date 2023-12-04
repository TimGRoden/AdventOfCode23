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
        static bool visualise = true;
        const int delay = 100;
        static int getID(string game) => int.Parse(game.Split(':')[0].Substring(5));
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
            if (visualise)
            {
                if (Console.CursorTop > Console.WindowHeight - 2) Console.Clear();
                Console.WriteLine(game);
                Console.Write($"MIN: {result[0]} red, {result[1]} green, {result[2]} blue. ");
                System.Threading.Thread.Sleep(delay);
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
            if (visualise)
            {
                if (Console.CursorTop > Console.WindowHeight - 2) Console.Clear();
                Console.WriteLine(game);
                Console.Write($"MIN: {result[0]} red, {result[1]} green, {result[2]} blue. ");
                System.Threading.Thread.Sleep(delay);
            }
            return result;
        }
        static int powerBeads(string game) => minBeads(game).Aggregate(1, (n, b) => n * b);
        static Game[] getGames(string[] contents)
        {
            Game[] games = new Game[contents.Length];
            for (int i = 0; i < contents.Length; i++) games[i] = new Game(contents[i], visualise, delay);
            return games;
        }
        static void Main(string[] args)
        {
            string[] contents = File.ReadAllLines("input.txt");
            if (visualise)
            {
                Console.WriteLine("Maximise, then press any key to begin.");
                Console.ReadKey(true);
                Console.Clear();
            }
            Game[] games = getGames(contents);
            int total = games.Aggregate(0,(n,g)=>n+(g.isValid()?g.ID:0));
            if (visualise)
            {
                Console.ReadKey(true);
                Console.Clear();
            }
            long powerTotal = games.Aggregate(0,(n,g)=>n + g.getPower());
            if (visualise) Console.ReadKey(true);
            Console.Clear();
            Console.WriteLine($"Part 1: {total}");
            Console.WriteLine($"Part 2: {powerTotal}");
            //int total = 0;

            //foreach (string game in contents)
            //{
            //    if (isValid(game)) total += getID(game);
            //}
            //Console.WriteLine($"Part1: {total}.");
            //long powerTotal = 0;
            //foreach (string game in contents) powerTotal += powerBeads(game);
            //Console.WriteLine($"Part2: {powerTotal}.");
            Console.ReadKey();

        }
    }
}

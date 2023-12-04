using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    public class Game
    {
        public int ID { get; private set; }
        private SubGame[] subGames;
        private bool visualise;
        private int delay;

        public Game(string game, bool visualise = false, int del = 100)
        {
            delay = del; this.visualise = visualise;
            ID = int.Parse(game.Split(':')[0].Substring(5));
            string[] subs = game.Split(':')[1].Split(';');
            subGames = new SubGame[subs.Length];
            for (int i = 0; i < subs.Length; i++) subGames[i] = new SubGame(subs[i]);
        }
        public string getSelf() => $"Game {ID}: "+subGames.Aggregate("",(s,g)=> s + g.printer()+"; ").Trim("; ".ToCharArray());
        private int[] countBeads()
        {
            if (visualise)
            {
                if (Console.CursorTop + 2 > Console.WindowHeight) Console.Clear();
                Console.Write(getSelf());
                System.Threading.Thread.Sleep(delay);
            }
            int[] result = { 0, 0, 0 };
            foreach (SubGame sub in subGames)
            {
                int[] subBeads = sub.storedBeads;
                for (int i = 0; i < subBeads.Length; i++) if (subBeads[i] > result[i]) result[i] = subBeads[i];
            }
            if (visualise)
            {
                if (Console.CursorTop > Console.WindowHeight - 2) Console.Clear();
                Console.Write($" => min: {result[0]} red, {result[1]} green, {result[2]} blue.");
                System.Threading.Thread.Sleep(delay);
            }
            return result;
        }
        public bool isValid()
        {
            if (visualise)
            {
                if (Console.CursorTop + 2 > Console.WindowHeight) Console.Clear();
                Console.Write(getSelf());
                Console.CursorLeft = 0;
                Console.Write($"Game {ID}: ");
                System.Threading.Thread.Sleep(delay);
            }
            for (int i = 0; i < subGames.Length; i++)
            {
                SubGame sub = subGames[i];
                if (!sub.isValid())
                {
                    if (visualise)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write(sub.printer());
                        if (i < subGames.Length - 1) Console.Write("; ");
                        System.Threading.Thread.Sleep(delay);
                        Console.CursorLeft = 0;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine(getSelf());
                        Console.ResetColor();
                    }
                    return false;
                }
                if (visualise)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Write(sub.printer());
                    Console.ResetColor();
                    if (i < subGames.Length - 1) Console.Write("; ");
                    System.Threading.Thread.Sleep(delay);
                }
            }
            if (visualise)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.CursorLeft = 0;
                Console.WriteLine(getSelf());
                Console.ResetColor();
            }
            return true;
        }

        public int getPower()
        {
            int result = countBeads().Aggregate(1, (n, b) => n * b);
            if (visualise)
            {
                Console.WriteLine($" => {result}.");
            }

            return result;
        }
    }
}

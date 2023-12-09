using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Day8
{
    internal class Program
    {
        static bool visualise = true;
        const int delay = 100;
        static Node[] MakeNodes(string[] contents)
        {
            Node[] nodes = new Node[contents.Length - 2];
            //Create nodes
            for (int i = 2; i < contents.Length; i++) nodes[i - 2] = new Node(contents[i]);
            //Link nodes
            for (int i = 0; i < nodes.Length; i++)
            {
                MatchCollection matches = Regex.Matches(contents[i + 2], @"([A-Z])\w+");
                Node curr = nodes[i];
                Node left = null; Node right = null;
                foreach (Node node in nodes)
                {
                    if (node.ID == matches[1].Value) left = node;
                    if (node.ID == matches[2].Value) right = node;
                }
                curr.linkNodes(left, right);
            }
            return nodes;
        }
        static void DelayPrint(string line)
        {
            Console.WriteLine(line);
            System.Threading.Thread.Sleep(delay);
        }
        static int Part1(Node[] nodes, string instr)
        {
            int count = 0;
            int pos = 0;
            Node curr = null;
            foreach (Node node in nodes)
            {
                if (node.ID == "AAA")
                {
                    curr = node;
                    if (visualise) DelayPrint("Found AAA.");
                    break;
                }
            }
            while (curr.ID != "ZZZ")
            {
                count++;
                switch (instr[pos])
                {
                    case 'L': if (visualise) DelayPrint($"{curr.getNode()} going left to {curr.LeftNode.ID}"); curr = curr.LeftNode; break;
                    case 'R': if (visualise) DelayPrint($"{curr.getNode()} going right to {curr.RightNode.ID}"); curr = curr.RightNode; break;
                }
                pos = (pos + 1)%instr.Length;
            }
            return count;

        }
        static long Part2(Node[] nodes, string instr)
        {
            int count = 0, pos = 0;
            List<Node> ghosts = new List<Node>();
            foreach (Node node in nodes) if (node.ID[2]=='A') ghosts.Add(node);
            if (visualise)
            {
                foreach (Node node in ghosts) Console.WriteLine($"Ghost {node.getNode()}.");
                Console.WriteLine("Press to begin Part 2");
                Console.ReadKey(true); Console.Clear();
            }
            List<long> counts = new List<long>();
            int ghostCount = ghosts.Count;
            while (counts.Count < ghostCount) //counts is the list of all "found" lengths.
            {
                count++;
                List<Node> newGhosts = new List<Node>();
                switch (instr[pos])
                {
                    case 'L':
                        for (int i = 0; i < ghosts.Count; i++)
                        {
                            if (visualise) DelayPrint($"Ghost {ghosts[i].ID} moving to {ghosts[i].LeftNode.ID}");
                            ghosts[i] = ghosts[i].LeftNode;
                            if (ghosts[i].ID[2] == 'Z') { counts.Add(count); if (visualise) DelayPrint($"Found {counts.Count} ghost-paths, length {counts.Last()}."); }
                            else newGhosts.Add(ghosts[i]); //This ghost isn't at z. keep going.
                        }
                        break;
                    case 'R':
                        for (int i = 0; i < ghosts.Count; i++)
                        {
                            if (visualise) DelayPrint($"Ghost {ghosts[i].ID} moving to {ghosts[i].RightNode.ID}");
                            ghosts[i] = ghosts[i].RightNode;
                            if (ghosts[i].ID[2] == 'Z') { counts.Add(count); if (visualise) DelayPrint($"Found {counts.Count} ghost-paths, length {counts.Last()}."); }
                            else newGhosts.Add(ghosts[i]);
                        }
                        break;
                }
                ghosts = newGhosts;
                pos = (pos + 1) % instr.Length;
            }
            
            return LCM(counts.ToArray());
        }
        static long LCM(long[] numbers)
        {
            return numbers.Aggregate(lcm);
        }
        static long lcm(long a, long b)
        {
            return Math.Abs(a * b) / GCD(a, b);
        }
        static long GCD(long a, long b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }
        static void Main(string[] args)
        {
            string[] contents = File.ReadAllLines("input.txt");
            Node[] nodes = MakeNodes(contents);
            if (visualise)
            {
                foreach (Node node in nodes) Console.WriteLine(node.getNode());
                Console.WriteLine("Press to begin part 1.");
                Console.ReadKey(true); Console.Clear();
            }

            Console.WriteLine($"Part 1: {Part1(nodes, contents[0])} steps.");
            if (visualise)
            {
                Console.WriteLine("Press clear.");
                Console.ReadKey(true); Console.Clear();
            }
            Console.WriteLine($"Part 2: {Part2(nodes, contents[0])} steps.");

            Console.ReadKey();
        }
    }
}

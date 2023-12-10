using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlTypes;

namespace Day10
{
    internal class Program
    {
        static bool visualise = true;
        const int delay = 10;
        static int[] getStartPos(string[] contents)
        {
            for (int y=0;y< contents.Length; y++)
            {
                for (int x = 0; x < contents[0].Length; x++)
                {
                    if (contents[y][x] == 'S') return new int[] { x, y };
                }
            }
            return new int[] { 0, 0 };
        }
        static PipeNode getNodeLoop(string[] contents)
        {
            int[] start = getStartPos(contents);
            PipeNode firstNode = new PipeNode('S', start[0], start[1]);
            //Connect forwards
            PipeNode curr = firstNode;
            do
            {
                if ((curr.type=='|' || curr.type=='L' || curr.type=='J' || curr.type=='S') &&
                    (contents[curr.yPos - 1][curr.xPos] == '|' || contents[curr.yPos - 1][curr.xPos] == '7' 
                    || contents[curr.yPos - 1][curr.xPos] == 'F' || contents[curr.yPos - 1][curr.xPos] == 'S'))
                { //Can go up
                    if (curr.prevNode == null ||( curr.yPos -1!=curr.prevNode.yPos || curr.xPos != curr.prevNode.xPos))
                    { //It's not the previous node.
                        if (contents[curr.yPos-1][curr.xPos] == 'S')
                        { //Is it the finish?
                            curr.nextNode = firstNode;
                            firstNode.prevNode = curr;
                            curr = firstNode; continue;
                        }
                        PipeNode next = new PipeNode(contents[curr.yPos - 1][curr.xPos], curr.xPos, curr.yPos - 1);
                        next.prevNode = curr;
                        curr.nextNode = next;
                        curr = next;
                        continue;
                    }
                }
                if ((curr.type == '-' || curr.type == 'L' || curr.type == 'F' || curr.type == 'S') && 
                    (contents[curr.yPos][curr.xPos + 1] == '-' || contents[curr.yPos][curr.xPos + 1] == '7' 
                    || contents[curr.yPos][curr.xPos + 1] == 'J' || contents[curr.yPos][curr.xPos + 1] == 'S'))
                { //Look right
                    if (curr.prevNode == null || (curr.yPos != curr.prevNode.yPos || curr.xPos+1 != curr.prevNode.xPos))
                    {
                        if (contents[curr.yPos][curr.xPos + 1] == 'S')
                        {
                            curr.nextNode = firstNode;
                            firstNode.prevNode = curr;
                            curr = firstNode; continue;
                        }
                        PipeNode next = new PipeNode(contents[curr.yPos][curr.xPos+1], curr.xPos+1, curr.yPos);
                        next.prevNode = curr;
                        curr.nextNode = next;
                        curr = next;
                        continue;
                    }
                }
                if ((curr.type == '|' || curr.type == 'F' || curr.type == '7' || curr.type == 'S') && 
                    (contents[curr.yPos+1][curr.xPos] == '|' || contents[curr.yPos+1][curr.xPos] == 'J' 
                    || contents[curr.yPos+1][curr.xPos] == 'L' || contents[curr.yPos+1][curr.xPos] == 'F'))
                { //look down
                    if (curr.prevNode == null ||( curr.yPos + 1 != curr.prevNode.yPos || curr.xPos != curr.prevNode.xPos))
                    {
                        if (contents[curr.yPos+1][curr.xPos ] == 'S')
                        {
                            curr.nextNode = firstNode;
                            firstNode.prevNode = curr;
                            curr = firstNode; continue;
                        }
                        PipeNode next = new PipeNode(contents[curr.yPos + 1][curr.xPos], curr.xPos, curr.yPos + 1);
                        next.prevNode = curr;
                        curr.nextNode = next;
                        curr = next;
                        continue;
                    }
                }
                if ((curr.type == '-' || curr.type == '7' || curr.type == 'J' || curr.type == 'S') && 
                    (contents[curr.yPos][curr.xPos - 1] == '-' || contents[curr.yPos][curr.xPos - 1] == 'F' 
                    || contents[curr.yPos][curr.xPos - 1] == 'L' || contents[curr.yPos][curr.xPos - 1] == 'S'))
                { //look left
                    if (curr.prevNode == null || (curr.yPos != curr.prevNode.yPos || curr.xPos - 1 != curr.prevNode.xPos))
                    {
                        if (contents[curr.yPos][curr.xPos - 1] == 'S')
                        {
                            curr.nextNode = firstNode;
                            firstNode.prevNode = curr;
                            curr = firstNode; continue;
                        }
                        PipeNode next = new PipeNode(contents[curr.yPos][curr.xPos - 1], curr.xPos - 1, curr.yPos);
                        next.prevNode = curr;
                        curr.nextNode = next;
                        curr = next;
                        continue;
                    }
                }

            } while (curr.type!='S');
            

            return firstNode;
        }
        static string[] padInput(string[] contents)
        {
            string[] padded = new string[contents.Length + 2];
            padded[0] = new string('.', contents.Length + 2);
            padded[padded.Length - 1] = new string('.', contents.Length + 2);
            for (int i = 0; i < contents.Length; i++) padded[i + 1] = "." + contents[i] + ".";
            return padded;
        }
        
        static void Main(string[] args)
        {
            string[] contents = padInput(File.ReadAllLines("input.txt"));
            PipeNode start = getNodeLoop(contents);
            if (visualise)
            {
                Console.WriteLine("Maximise then press to continue.");
                Console.ReadKey(true); Console.Clear();
                foreach (string line in contents) Console.WriteLine(line);
            }
            int sol = start.getLoopLength(visualise, delay);
            if (visualise) Console.SetCursorPosition(0, contents.Length + 1);
            Console.ResetColor();
            Console.WriteLine($"Max Path Length: {sol}.");





            Console.ReadKey();
        }
    }
}

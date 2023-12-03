using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Xml.XPath;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Day3
{
    internal class Program
    {
        const bool debugging = true;
        static bool hasSymbol(string[] space, int C1, int C2, int R1, int R2)
        {
            for (int row = R1; row <= R2; row++)
            {
                if (row >= space.Length || row < 0) continue;
                for (int col = C1; col <= C2; col++)
                {
                    if (col < 0 || col >= space[0].Length) continue;
                    if (space[row][col]!= '.' && !char.IsNumber(space[row][col])) return true;
                }
            }
            return false;
        }
        static int getNum(string[] space, int col, int row)
        {
            int c1 = col, len = 0 ;
            do { c1--; } while (c1 >= 0 && char.IsNumber(space[row][c1]));
            c1++;
            while (c1 + len < space[0].Length && char.IsNumber(space[row][c1 + len])) len++;
            if (debugging)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.SetCursorPosition(c1, row);
                Console.Write(space[row].Substring(c1, len));
                Console.ResetColor();
            }
            return int.Parse(space[row].Substring(c1, len));
        }
        static int calcGear(string[] space, int col, int row)
        {
            if (!isGear(space, col, row)) return 0;
            List<int> foundVals = new List<int>();
            for (int r = row-1; r <= row + 1; r++)
            {
                for (int c = col-1; c <= col + 1; c++)
                {
                    if (char.IsNumber(space[r][c])) foundVals.Add(getNum(space, c, r));
                }
            }
            foundVals.OrderBy(x => x); //Deals with spotting the same number several times.
            return foundVals[0] * foundVals[foundVals.Count-1];
        }
        static bool isGear(string[] space, int col, int row)
        {
            if (debugging) { 
                Console.SetCursorPosition(col, row); 
                Console.BackgroundColor = ConsoleColor.DarkMagenta; 
                Console.Write('*'); 
                Console.ResetColor(); }
            bool[] positions = new bool[9]; //left to right, top to bottom.
            for (int i = -1; i < 2; i++)
            {
                if (row + i >= space.Length || row + i < 0)
                {
                    positions[(i + 1) * 3] = false; 
                    positions[(i + 1) * 3 + 1] = false; 
                    positions[(i + 1) * 3 + 2] = false; continue; //Not a valid row.
                }
                for (int j = -1; j < 2; j++)
                {
                    if (col + j >= space[0].Length || col + j < 0)
                    {
                        positions[(j+1) + (i+1) * 3] = false; continue; //Not a valid location.
                    }
                    positions[(j + 1) + (i + 1) * 3] = char.IsNumber(space[row+i][col+j]); //Is it a number?
                }
            }
            if (positions[0] && !positions[1] && positions[2]) return true; //X-X
            if (positions[3] && positions[5]) return true; //middle is the *
            if (positions[6] && !positions[7] && positions[8]) return true; //X-X
            bool[][] rows = {   new bool[]{ positions[0], positions[1], positions[2] }, 
                                new bool[] { positions[3], positions[4], positions[5] }, 
                                new bool[] { positions[6], positions[7], positions[8] } };
            if (rows[0].Contains(true) && (rows[1].Contains(true) || rows[2].Contains(true))) return true; //Different rows
            if (rows[1].Contains(true) && rows[2].Contains(true)) return true;
            if (debugging) { 
                Console.SetCursorPosition(col, row); 
                Console.BackgroundColor = ConsoleColor.DarkBlue; 
                Console.Write('*'); 
                Console.ResetColor(); }
            return false;
        }
        static int Part1(string[] space)
        {
            int total = 0;
            for (int row = 0;row < space.Length; row++)
            {
                for (int col = 0; col < space[0].Length; col++)
                {
                    if (char.IsNumber(space[row][col]))
                    { //Found a number. Track its bounds.
                        int C1 = col-1; int R1 = row-1; int R2 = row + 1;
                        col++;
                        while (col < space[0].Length && char.IsNumber(space[row][col])) col++;
                        int C2 = col;
                        if (hasSymbol(space, C1, C2, R1, R2))
                        {
                            total += int.Parse(space[row].Substring(++C1, C2 - C1));
                            //if (debugging) Highlight(space, row, C1, C2);
                        } //else if (debugging) Highlight(space, row, ++C1, C2, ConsoleColor.DarkYellow);
                    }
                }
            }
            return total;
        }
        static int Part2(string[] space)
        {
            int total = 0;
            for (int row = 0;row< space.Length; row++)
            {
                for (int col = 0; col < space[0].Length; col++)
                {
                    if (space[row][col] == '*') total += calcGear(space, col, row);
                }
            }
            return total;
        }
        static void Highlight(string[] contents, int row, int c1, int c2, ConsoleColor shade = ConsoleColor.DarkMagenta)
        {
            Console.BackgroundColor = shade;
            Console.SetCursorPosition(c1, row);
            Console.Write(contents[row].Substring(c1, c2 - c1));
            Console.ResetColor();
        }
        static void Main(string[] args)
        {
            if (debugging) Console.ReadKey(true);
            string[] contents = File.ReadAllLines("input.txt");
            if (debugging) foreach (string line in contents) Console.WriteLine(line);
            Console.WriteLine($"Part 1: {Part1(contents)}.");
            Console.WriteLine($"Part 2: {Part2(contents)}.");
            Console.ReadKey();
        }
    }
}

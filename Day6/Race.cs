using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    public class Race
    {
        public long time { get; private set; }
        public long record { get; private set; }
        public Race(long t, long r) => (time, record) = (t, r);
        public long winOpts(bool visualise = false, int delay = 100)
        {
            double disc = Math.Pow(time, 2) - 4 * record;
            if (disc < 0) return 0; //No ways to win.
            long n_1 = (long)Math.Floor((time - Math.Sqrt(disc)) / 2) + 1;
            long n_2 = (long)Math.Ceiling((time + Math.Sqrt(disc)) / 2) - 1;
            if (visualise)
            {
                Console.WriteLine($"Race: {time} ms, {record} mm record.");
                System.Threading.Thread.Sleep(delay);
                Console.WriteLine($"First winning hold: {n_1} ms");
                System.Threading.Thread.Sleep(delay);
                Console.WriteLine($"Last winning hold: {n_2} ms");
                System.Threading.Thread.Sleep(delay);
                Console.WriteLine($"Winning options: {n_2 - n_1 + 1}");
                System.Threading.Thread.Sleep(delay);
                Console.WriteLine();
            }
            return n_2 - n_1 + 1;
        }
    }
}

using System;
using AoC2022;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = Common.ParseFile(@"input.txt");
            CPU.RunInstructions(lines);
            Console.WriteLine($"Part 1 Score: {CPU.sumOfSignalStrengths}\n"+
                               "Part 2 Output:");
            GPU.OutputVideoBuffer();
        }
    }
}

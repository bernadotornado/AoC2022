using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using AoC2022;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = Common.ParseFile("input.txt");
            Console.WriteLine($"Part 1 Score: {Monkey.GetMonkeyBusinessScore(lines, true, 20)}\n" +
                              $"Part 2 Score: {Monkey.GetMonkeyBusinessScore(lines, false, 10_000)}");
        }
    }
}

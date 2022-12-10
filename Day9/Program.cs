using System;
using System.Collections.Generic;
using AoC2022;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Knot.Pos> SimulateRope(List<string> list, int ropeLength)
            {
                Knot head = new Knot(true, ropeLength);
                List<Knot.Pos> visited = new List<Knot.Pos>();
                foreach (var line in list)
                {
                    var arg = line.Split(" ");
                    var operation = arg[0];
                    var amount = int.Parse(arg[1]);
                    for (int i = 0; i < amount; i++)
                        head.Update(operation, visited);
                }
                return visited;
            }
            var lines = Common.ParseFile(@"input.txt");
            Console.WriteLine($"Part 1 Score: {SimulateRope(lines, 2).Count}\n" +
                              $"Part 2 Score: {SimulateRope(lines, 10).Count}\n");
           
        }
    }
}

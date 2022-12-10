using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using AoC2022;

namespace Day10
{

    
    class Program
    {
        static void Main(string[] args)
        {
            var lines = Common.ParseFile(@"input.txt");

            var cpu = new CPU();
            cpu.ReadInstructions(lines);
            cpu.RunInstructions();
            Console.WriteLine("Part 1 Score: "+cpu.sumOfSignalStrengths);
            Console.WriteLine("Part 2 Output:\n");
            int index = 1;
            foreach (var c in cpu.videoBuffer)
            {
                Console.Write(c);
                if (index % 40 == 0)
                {
                    Console.WriteLine();
                    index = 0;
                }
                index++;
            }
            //Console.WriteLine(cpu.sumOfSignalStrengths);
        }
    }
}

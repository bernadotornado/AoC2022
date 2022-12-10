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
            Console.WriteLine($"Part 1 Score: {cpu.sumOfSignalStrengths}\n"+
                               "Part 2 Output:");
            CPU.iGPU.OutputVideoBuffer();
           
        }
    }
}

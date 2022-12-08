using AoC2022;
using System;
using System.Collections.Generic;

namespace Day3
{
    class Program
    {
        const int A = 65-27;
        const int a = 97-1;
        static int EvaluateChar(char c) => c - (c >= 97 ? a : A); 
        static int[] score = new int[] { 0, 0 };
        static char GetCommonChar(string a, string b)
        {
            for (int i = 65; i < 124; i++)
                if(a.Contains((char)(i)) && b.Contains((char)(i)))
                    return (char)i;
            return '?';
        }
        static char GetCommonChar(string[] group)
        {
            foreach(char c in group[0])
                if(group[1].Contains(c)&& group[2].Contains(c))
                    return c;
            return '?';
        }
        static void Main(string[] args)
        {
            var lines = Common.ParseFile(@"input.txt");
            int count = 0;
            foreach (string rucksack in lines)
            {
                int compartmentSize = rucksack.Length / 2;
                var firstCompartment = rucksack.Substring(0, compartmentSize);
                var secondCompartment = rucksack.Substring(compartmentSize);
                char common = GetCommonChar(firstCompartment, secondCompartment);
                score[0] += EvaluateChar(common);

                if (count % 3 == 0)
                    score[1] += EvaluateChar(GetCommonChar(new string[] { lines[count], lines[count + 1], lines[count + 2] }));
                
                count++;
            }
            Console.WriteLine($"Part 1 Score: {score[0]}\n" +
                              $"Part 2 Score: {score[1]}\n");
        }
    }
}

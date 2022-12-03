using AoC2022;
using System;

namespace Day3
{
    class Program
    {
        const int A = 65-27;
        const int a = 97-1;

        // static int EvaluateChar(char c) => (int)(c) - ((int)(c) >= 97 ? a : A);
        static int EvaluateChar(char c) {
            
            if(c == '?')
            {
                Console.ReadKey();
            }

            return (int)(c) - ((int)(c) >= 97 ? a : A); }
        static int score = 0;
        static char GetCommonChar(string a, string b)
        {
            for (int i = 65; i < 123; i++)
            {
                if(a.Contains((char)(i)) && b.Contains((char)(i)))
                {
                    return (char)i;
                }
            }
            return '?';
        }
        static void Main(string[] args)
        {
            var lines = Common.ParseFile(@"input.txt");
            foreach (var item in lines)
            {
                string rucksack = item;
                int compartmentSize = rucksack.Length / 2;
                var firstCompartment = rucksack.Substring(0, compartmentSize);
                var secondCompartment = rucksack.Substring(compartmentSize, compartmentSize);
                char common = GetCommonChar(firstCompartment, secondCompartment);
                
                score += EvaluateChar(common);
                Console.WriteLine($"rucksack: {rucksack} + {rucksack.Length}\n" +
                    $"firstCompartment: {firstCompartment} + {firstCompartment.Length}\n" +
                    $"secondCompartment: {secondCompartment} + {secondCompartment.Length}\n" +
                    $"common: {common}\n" +
                    $"score: {EvaluateChar(common)}\n" +
                    $"total: {score}");
                
                   // Console.ReadKey();
            }
            //for (int i = 65; i < 123; i++)
            //{
            //  Console.WriteLine($"char: {(char)i} +" + EvaluateChar((char)i));
            //}
        }
    }
}

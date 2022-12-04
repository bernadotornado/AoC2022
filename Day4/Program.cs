using System;
using System.Collections.Generic;
using AoC2022;

namespace Day4
{
    class Program
    {
        static int[] score = new int[2] { 0, 0 };

        static void Main(string[] args)
        {
            var lines = Common.ParseFile(@"input.txt");
            foreach (var item in lines)
            {
                var a = item.Split('-', ',');
                List<int> p1 = new List<int>();
                List<int> p2 = new List<int>();
                var pair1 = new int[2] { int.Parse(a[0]), int.Parse(a[1]) };
                var pair2 = new int[2] { int.Parse(a[2]), int.Parse(a[3]) };
                bool pair1ContainsPair2 = pair1[0] <= pair2[0] && pair1[1] >= pair2[1];
                bool pair2ContainsPair1 = pair2[0] <= pair1[0] && pair2[1] >= pair1[1];

                if (pair2ContainsPair1 || pair1ContainsPair2)
                    score[0]++;
                
               for (int i = pair1[0]; i <= pair1[1]; i++)
                    p1.Add(i);
               
               for (int i = pair2[0]; i <= pair2[1]; i++)
                   p2.Add(i);

               foreach (var i in p1)
                    if (p2.Contains(i))
                    {
                        score[1]++;
                        break; 
                    }

            }
            Console.WriteLine($"Part 1 Score: {score[0]}\n" +
                              $"Part 2 Score: {score[1]}\n");
        }
    }
}

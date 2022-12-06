using System;
using System.Collections.Generic;
using AoC2022;

namespace Day6
{
    class Program
    {

        static int StartOfMarker(List<String> lines, int packetSize)
        {
            var line = lines[0];
            for (int i = packetSize - 1; i < line.Length; i++)
            {
                var list = new List<char>();
                for (int j = packetSize - 1; j >= 0; j--)
                    list.Add(line[i - j]);

                bool b = true;
                var temp = list.ToArray();
                for (int k = 0; k < list.Count; k++)
                {
                    var currentChar = list[k];
                    list.RemoveAt(k);
                    b = b && !list.Contains(currentChar);
                    list.Clear();
                    list.AddRange(temp);
                }
                if (b)
                    return (i + 1);
            } return -1;
        }

        static void Main(string[] args)
        {
            var lines = Common.ParseFile(@"input.txt");
            int[] score = new[]
            {
                StartOfMarker(lines, 4),
                StartOfMarker(lines, 14)
            };
            
            Console.WriteLine($"Part 1 Score: {score[0]}\n" +
                              $"Part 2 Score: {score[1]}\n");
        }
    }
}

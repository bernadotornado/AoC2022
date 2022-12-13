using System;
using System.Collections.Generic;
using AoC2022;
namespace Day1
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<int> sums = new List<int>();
            var lines = Common.ParseFile(@"input.txt");
            int currentNum = 0;
            foreach(var item in lines)
            {
                if(item == "")
                {
                    sums.Add(currentNum);
                    currentNum = 0;
                    continue;
                }
                currentNum += Convert.ToInt32(item);
            }
            sums.Sort();
            Console.WriteLine($"Part 1 Score: {sums[sums.Count-1]}\n" +
                                $"Part 2 Score: {sums[sums.Count - 1]+ sums[sums.Count - 2]+ sums[sums.Count - 3]}");
        }
    }
}

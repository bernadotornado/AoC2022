using System;
using System.Linq;
using AoC2022;
using System.Collections.Generic;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Stack<Char>> vs = new List<Stack<char>>();
            for (int i = 0; i < 9; i++)
                vs.Add(new Stack<char>());
            var lines = Common.ParseFile(@"input.txt");


            int headerLength = 8;
            for (int i = 0; i < headerLength; i++)
                for (int j = 1, index = 0; j < lines[i].Length; j+=4, index++)
                    if (lines[i][j] != ' ')
                        vs[index].Push(lines[i][j]);
                
            Console.WriteLine(lines);
        }
    }
}

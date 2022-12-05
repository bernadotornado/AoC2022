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


            for (int i = 0; i < 8; i++)
            {
                char[] c = lines[i].ToCharArray();
                //foreach (var item in c)
                //{
                //    if(item != ' ' && item != '[' && item != ']')
                //    {
                //        Console.WriteLine(item);
                //    }
                //}
                for (int j = 1; j < lines[i].Length; j+=4)
                {
                    Console.WriteLine(lines[i][j]);
                    if (lines[i][j] != ' ')
                    {
                        vs[j].Push(lines[i][j]);
                    }
                }
            }
            //var stackAmount = from line in lines
            //                  where line.Trim().ToCharArray()[0] == 1
            //                  select line;
            //Console.WriteLine(stackAmount.FirstOrDefault());


            
        }
    }
}

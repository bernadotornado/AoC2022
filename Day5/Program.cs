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
            int amountOfStacks = 9;
            int headerLength = 8;
            List<Stack<Char>> stacks = new List<Stack<char>>();
            var lines = Common.ParseFile(@"input.txt");
            for (int i = 0; i < amountOfStacks; i++)
                stacks.Add(new Stack<char>());
            
            //Fill Stacks
            for (int row = headerLength-1; row >= 0; row--)
                for (int posOfCharInString = 1, index = 0; posOfCharInString < lines[row].Length; posOfCharInString+=4, index++)
                    if (lines[row][posOfCharInString] != ' ') 
                        stacks[index].Push(lines[row][posOfCharInString]);

            //Run Intstructions
            for (int i = headerLength+2; i < lines.Count; i++)
            {
                var a = lines[i].Split(" ");
               
                 for (int j = 0; j < int.Parse(a[1]); j++)
                 {
                      int from = int.Parse(a[3])-1;
                      int to = int.Parse(a[5])-1;
                      stacks[to].Push(stacks[from].Pop());
                 }
            }
            foreach (var stack in stacks)
                Console.Write(stack.Peek());
        }
    }
}

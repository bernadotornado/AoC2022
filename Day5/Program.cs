﻿using System;
using System.Linq;
using AoC2022;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            bool grabOneByOne = false;
            int amountOfStacks = 9;
            int headerLength = 8;


            //Run Instructions
            List<Stack<Char>> RunInstructions(bool grabbingMethod)
            {
                List<Stack<Char>> stacks = new List<Stack<char>>();
                var lines = Common.ParseFile(@"input.txt");
                for (int i = 0; i < amountOfStacks; i++)
                    stacks.Add(new Stack<char>());

                //Fill Stacks
                for (int row = headerLength - 1; row >= 0; row--)
                for (int posOfCharInString = 1, index = 0;
                    posOfCharInString < lines[row].Length;
                    posOfCharInString += 4, index++)
                    if (lines[row][posOfCharInString] != ' ')
                        stacks[index].Push(lines[row][posOfCharInString]);

                for (int i = headerLength + 2; i < lines.Count; i++)
                {
                    var a = lines[i].Split(" ");

                    if (grabbingMethod)
                        for (int j = 0; j < int.Parse(a[1]); j++)
                        {
                            int from = int.Parse(a[3]) - 1;
                            int to = int.Parse(a[5]) - 1;
                            stacks[to].Push(stacks[from].Pop());
                        }
                    else
                    {
                        List<Char> grabbed = new List<char>();
                        int to = int.Parse(a[5]) - 1;
                        int from = int.Parse(a[3]) - 1;
                        for (int j = 0; j < int.Parse(a[1]); j++)
                        {

                            grabbed.Add(stacks[from].Pop());

                        }

                        for (int j = grabbed.Count - 1; j >= 0; j--)
                        {
                            stacks[to].Push(grabbed[j]);
                        }
                    }
                }

                return stacks;
            }

            var stacks = new List<Stack<Char>>[]
            {
                RunInstructions(true),
                RunInstructions(false)
            };

            foreach (var s in stacks)
            {
                foreach (var stack in s)
                    Console.Write(stack.Peek());
                Console.WriteLine();
            }
        }
    }
}

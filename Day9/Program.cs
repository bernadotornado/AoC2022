using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AoC2022;
using Microsoft.VisualBasic.CompilerServices;

namespace Day9
{
    class Program
    {
        private static int iter = 0;

        class Knot
        {
            public Pos pos = new Pos(0,0);
            public Knot next;
            public Knot parent;
            public Pos last;
            private static Knot head;
            private static Knot tail;

            public Knot(bool isHead, int length)
            {
                if (isHead)
                    head = this;
                if(!(length> 1))
                    tail = this;
                else
                {
                    next = new Knot(false, length - 1);
                    next.parent = this;
                }
            }
            public void Update(string c, List<Pos> visited)
            {
                
                    
                    
                        last = pos;
                        if (head == this)
                        {
                            switch (c)
                            {
                                case "R":
                                    pos.x++; break;
                                case "L": 
                                    pos.x--; break;
                                case "U":
                                    pos.y++; break;
                                case "D":
                                    pos.y--; break;
                            }
                            next.Update(c, visited);
                        }
                        else
                        {
                            if (Math.Abs(parent.pos.x - pos.x) > 1 || Math.Abs(parent.pos.y - pos.y) > 1)
                            {
                                pos = parent.last;
                            }
                            if (this == tail)
                            {
                                if (!visited.Contains(pos))
                                {
                                    visited.Add(tail.pos);
                                    
                                }
                            }
                            else next.Update(c, visited);
                        }
                    
                        // TODO: FIX THIS MESS
                

                
            }
            
            
        }
        struct Pos
        {
            public int x;
            public int y;
            public Pos(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        static void Main(string[] args)
        {
            List<Pos> SimulateRope(List<string> list, int ropeLength)
            {
                Knot _head = new Knot(true, ropeLength);
                List<Pos> visited = new List<Pos>();
                foreach (var line in list)
                {
                    var arg = line.Split(" ");
                    var operation = arg[0];
                    var amount = int.Parse(arg[1]);
                    for (int i = 0; i < amount; i++)
                        _head.Update(operation, visited);
                }
                return visited;
            }
            var lines = Common.ParseFile(@"input.txt");
            Console.WriteLine($"Part 1 Score: {SimulateRope(lines, 2).Count}\n" +
                              $"Part 2 Score: {SimulateRope(lines, 10).Count}\n");
           
        }
    }
}

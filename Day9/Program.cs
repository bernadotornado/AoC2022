using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AoC2022;

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
                if (length > 1)
                {
                    next = new Knot(false, length - 1);
                    next.parent = this;
                }
                else
                {
                    tail = this;
                }
            }
            public void Update(string line, List<Pos> visited)
            {
                
                    var a = line.Split(" ");
                    for (int i = 0; i < int.Parse(a[1]); i++)
                    {
                        last = pos;
                        if (head == this)
                        {
                            switch (a[0])
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
                        }
                        if(parent != null)
                            if (Math.Abs(parent.pos.x - pos.x) > 1 || Math.Abs(parent.pos.y - pos.y) > 1)
                            {
                                
                                pos = parent.last;
                            }

                        if (this == tail && !visited.Contains(pos))
                        {
                            visited.Add(tail.pos);
                        }
                        
                        if (next != null )
                            next.Update(line, visited);
                    }
                    iter++;
                  
                

                
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

            
            var lines = Common.ParseFile(@"test2.txt");
            Console.WriteLine("Lines: "+lines.Count);
            Knot _head = new Knot(true, 10);
            
            List<Pos> visited = new List<Pos>();
            int lin = 0;
            foreach (var item in lines)
            {
                _head.Update(item, visited);
                lin++;
                Console.WriteLine(item+ " iter: "+ iter+ " line: "+lin);
                if (lin == 2)
                {
                    Console.WriteLine(_head);
                }
            }

            Console.WriteLine($"Part 1 Score: {visited.Count}\n" +
                              $"Part 2 Score: {0}\n");
           
        }
    }
}

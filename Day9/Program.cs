using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AoC2022;

namespace Day9
{
    class Program
    {

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
                 //   tail.parent
                }
            }
            public void Update(List<string> lines, List<Pos> visited)
            {
                foreach (var item in lines)
                {
                    var a = item.Split(" ");
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
                            next.Update(lines, visited);
                    }
                }

                
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

            Knot _head = new Knot(true, 2);
            
            
            var lines = Common.ParseFile(@"test.txt");
            // Pos head = new Pos(0, 0);
            // Pos tail = new Pos(0, 0);
            // Pos last = head;
            List<Pos> visited = new List<Pos>();
            // foreach (var item in lines)
            // {   var a = item.Split(" ");
            //     for(int i = 0; i< int.Parse(a[1]); i++)
            //     {
            //         last = head;
            //         switch (a[0])
            //         {
            //             case "R":
            //                 head.x++; break;
            //             case "L": 
            //                 head.x--; break;
            //             case "U":
            //                 head.y++; break;
            //             case "D":
            //                 head.y--; break;
            //         }
            //
            //         if (Math.Abs(tail.x - head.x) > 1 || Math.Abs(tail.y - head.y) > 1)
            //             tail = last;
            //         
            //         if(!visited.Contains(tail))
            //             visited.Add(tail);
            //     }
            //}
            _head.Update(lines, visited);
            
            Console.WriteLine($"Part 1 Score: {visited.Count}\n" +
                              $"Part 2 Score: {0}\n");
           
        }
    }
}

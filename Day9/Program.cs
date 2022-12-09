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
            private static Knot head;

            public Knot(bool isHead, int length)
            {
                if (isHead)
                    head = this;
                if (length > 1)
                    next = new Knot(false, length - 1);
            }
            public void Update()
            {
                
                
                
                
                if (next != null)
                    next.Update();
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

            Knot _head = new Knot(true, 10);
            
            
            var lines = Common.ParseFile(@"input.txt");
            Pos head = new Pos(0, 0);
            Pos tail = new Pos(0, 0);
            Pos last = head;
            List<Pos> visited = new List<Pos>();
            foreach (var item in lines)
            {   var a = item.Split(" ");
                for(int i = 0; i< int.Parse(a[1]); i++)
                {
                    last = head;
                    switch (a[0])
                    {
                        case "R":
                            head.x++; break;
                        case "L": 
                            head.x--; break;
                        case "U":
                            head.y++; break;
                        case "D":
                            head.y--; break;
                    }

                    if (Math.Abs(tail.x - head.x) > 1 || Math.Abs(tail.y - head.y) > 1)
                        tail = last;
                    
                    if(!visited.Contains(tail))
                        visited.Add(tail);
                }
            }
            
            Console.WriteLine($"Part 1 Score: {visited.Count}\n" +
                              $"Part 2 Score: {0}\n");
           
        }
    }
}

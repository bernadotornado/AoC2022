using  System;
using  System.Collections.Generic;

namespace Day9
{
    public class Knot
    {
            public struct Pos
            {
                public int x;
                public int y;
                public Pos(int x, int y)
                {
                    this.x = x;
                    this.y = y;
                }
            }
            
            public Pos pos = new Pos(0,0);
            public Knot next;
            public Knot parent;
            private static Knot head;
            private static Knot tail;

            public Knot(bool isHead, int length)
            {
                if (isHead)
                    head = this;
                if (length <= 1)
                    tail = this;
                else
                {
                    next = new Knot(false, length - 1);
                    next.parent = this;
                }
            }
            public void Update(string c, List<Pos> visited)
            {
                if (head == this)
                    MoveHead(c);
                else
                    FollowKnot(visited);
                if(this != tail) 
                    next.Update(c, visited);
            }

            private void MoveHead(string c)
            {
                switch (c)
                {
                    case "R": pos.x++;  break;
                    case "L": pos.x--;  break;
                    case "U": pos.y++;  break;
                    case "D": pos.y--;  break;
                }
            }

            private void FollowKnot(List<Pos> visited)
            {
                int stepX = parent.pos.x - pos.x;
                int stepY = parent.pos.y - pos.y;

                if (Math.Abs(stepX) > 1 || Math.Abs(stepY) > 1)
                {
                    pos.x += Math.Sign(stepX);
                    pos.y += Math.Sign(stepY);
                }

                if (this == tail)
                    if (!visited.Contains(pos))
                        visited.Add(tail.pos);
            }
    }
}
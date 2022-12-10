using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    public class Sprite
    {
        private int pos;

        public bool SpriteIsInCycle(int cycle) => (pos == cycle) || (pos == cycle - 1) || (pos == cycle + 1);
        public void SetPos(int x)
        {
            pos = x;
            Console.WriteLine($"SpritePos: {x}");
        }

        public int GetPos() => pos;

        public Sprite(int startPos)
        {
            pos = startPos;
        }
    }
    public class CPU
    {
        public struct Operation
        {
            public string op;
            public int arg;

            public Operation(string op, int arg)
            {
                this.arg = arg;
                this.op = op;
            }
        }

        public List<Char> videoBuffer = new List<char>();
        private Sprite sprite = new Sprite(1);
        public int x = 1;
        int[] signalStrengths = { 20, 60, 100, 140, 180, 220 };
        public int sumOfSignalStrengths = 0;
        private int cycle =1;
        public Queue<Operation> Operations = new Queue<Operation>();

        public void NoOp()
        {
            //Console.WriteLine("Begin noop");
            DrawPixel();
            Cycle++;
            //Console.WriteLine("End noop");
        }

        void DrawPixel()
        {
            char c;
            int temp = cycle % 40;
            if (sprite.SpriteIsInCycle(temp -1))
            {
                c = '#';
            }
            else
            {
                c='.';
            }
            Console.WriteLine($"Cycle: {cycle} Drawing Pixel in Pos: {videoBuffer.Count}; Pixel: {c}, SpritePos:{sprite.GetPos()}, temp: {temp}");
            videoBuffer.Add(c);
        }
        public void AddX(int arg)
        {
            Console.WriteLine($"Cycle: {cycle} Begin executing addx {arg}");
            DrawPixel();
            Cycle++;
            DrawPixel();
            Cycle++;
            x+= arg;
            sprite.SetPos(x);
            Console.WriteLine($"Cycle: {cycle} End addx {arg}. x: {x}");
        }

        public void ReadInstructions(List<string> lines)
        {
            foreach (var s in lines)
            {
                var a = s.Split(" ");
                Operation op = new Operation(a[0], a.Length>1 ? int.Parse(a[1]):0);
                Operations.Enqueue(op);
            }
          
        }
        public void RunInstructions()
        {
            while (Operations.Count > 0)
            {
                var op = Operations.Dequeue();
                switch (op.op)
                {
                    case "noop" :
                        NoOp();
                        break;
                    case "addx":
                        AddX(op.arg);
                        break;
                }
                
            }
        }
        public int Cycle
        {
            get
            {
               // Console.WriteLine("Cycle: "+cycle);
                if (signalStrengths.Contains(cycle))
                {
                    sumOfSignalStrengths += cycle * x;
                }

               
                
                return cycle;
            }
            set
            {
                cycle = value;
            }
        }
    }
}
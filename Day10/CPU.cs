using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    
    public class CPU
    {
        public class iGPU
        {
            public static List<char> videoBuffer = new List<char>();
            public class Sprite
            {
                private int pos;
                public bool SpriteIsInCycle(int cycle) => pos == cycle || pos == cycle - 1 || pos == cycle + 1;
                public void SetPos(int x) => pos = x;
                public Sprite(int startPos) => pos = startPos;
            }
        }
        
        
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

        private iGPU.Sprite sprite = new iGPU.Sprite(1);
        public int x = 1;
        int[] signalStrengths = { 20, 60, 100, 140, 180, 220 };
        public int sumOfSignalStrengths = 0;
        private int cycle = 1;
        public Queue<Operation> Operations = new Queue<Operation>();

        public void noop() => Cycle++;

        void DrawPixel()
        {
            int pixelpos = (cycle % 40)-1;
            iGPU.videoBuffer.Add(sprite.SpriteIsInCycle(pixelpos)?'#':' ');
        }
        public void addx(int arg)
        {
            Cycle++;
            Cycle++;
            x+= arg;
            sprite.SetPos(x);
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
                        noop();
                        break;
                    case "addx":
                        addx(op.arg);
                        break;
                }
                
            }
        }
        public int Cycle
        {
            get
            {
                if (signalStrengths.Contains(cycle))
                    sumOfSignalStrengths += cycle * x;
                DrawPixel();
                return cycle;
            }
            set => cycle = value;
        }
    }
}
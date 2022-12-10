using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
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
        
        public int x = 1;
        int[] signalStrengths = { 20, 60, 100, 140, 180, 220 };
        public int sumOfSignalStrengths = 0;
        private int cycle =1;
        public Queue<Operation> Operations = new Queue<Operation>();

        public void NoOp()
        {
            //Console.WriteLine("Begin noop");
            Cycle++;
            //Console.WriteLine("End noop");
        }

        public void AddX(int arg)
        {
            //Console.WriteLine($"Begin addx {arg}");
            Cycle++;
            Cycle++;
            x+= arg;
            //Console.WriteLine($"End addx {arg}. x: {x}");
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
             //   Console.WriteLine("Cycle: "+cycle);
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
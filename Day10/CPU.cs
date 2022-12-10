using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    public static class CPU
    {
        private static int x = 1;
        public static int sumOfSignalStrengths = 0;
        private static int cycle = 1;
        private static Queue<(string, int)> operations = new Queue<(string, int)>();
        
        public static void RunInstructions(List<string> lines)
        {
            foreach (var s in lines)
            {
                 
                var a = s.Split(" ");
                var (op, arg) = (a[0], a.Length > 1 ? int.Parse(a[1]) : 0);
                switch (op)
                {
                    case "noop" :
                        Cycle++;
                        break;
                    case "addx":
                        Cycle++;
                        Cycle++;
                        x+= arg;
                        GPU.SpritePos = x;
                        break;
                }
            }
        }
        static int Cycle
        {
            get
            {
                if ((cycle - 20)% 40 == 0)
                    sumOfSignalStrengths += cycle * x;
                GPU.AddPixelToBuffer(cycle);
                return cycle;
            }
            set => cycle = value;
        }
    }
}
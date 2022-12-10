using  System;
using System.Collections.Generic;

namespace Day10
{
    public class GPU
    {
        public static int SpritePos = 1;
        public static bool SpriteIsInCycle(int cycle) => SpritePos == cycle || SpritePos == cycle - 1 || SpritePos == cycle + 1;
        public static List<char> videoBuffer = new List<char>();
        public static void AddPixelToBuffer(int cycle )
        {
            int pixelpos = (cycle % 40)-1;
            videoBuffer.Add(SpriteIsInCycle(pixelpos)?'█':' ');
        }
        public static void OutputVideoBuffer()
        {
            int index = 1;
            foreach (var c in videoBuffer)
            {
                Console.Write(c);
                if (index % 40 == 0)
                    Console.WriteLine();
                index++;
            }
        }
    }
}
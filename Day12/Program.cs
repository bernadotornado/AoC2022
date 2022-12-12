using System;
using AoC2022;

namespace Day12
{
    class Program
    {
        const int A = 65 - 27;
        const int a = 97 - 1;
        static int EvaluateChar(char c) => (c - (c >= 97 ? a : A))-1;
        private static (int, int) startpos;
        private static (int, int) endpos;
        static int SetStart(int x, int y)
        {
            startpos = (x, y);
            return 0;
        }
        static int SetEnd(int x, int y)
        {
            endpos = (x, y);
            return 0;
        }
        
        static void Main(string[] args)
        {
            
            var lines = Common.ParseFile("input.txt");
            
            int width = lines[0].Length;
            int height = lines.Count;
            var map = new int[width, height];
            
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    map[x, y] = lines[y][x] switch
                    {
                        'S' => EvaluateChar('a') + SetStart(x,y),
                        'E' => EvaluateChar('z') + SetEnd(x,y),
                         _  => EvaluateChar(lines[y][x])
                    };

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                    Console.Write(map[x, y]);
                Console.WriteLine();
            }
            
            Console.WriteLine(map);
        }
    }
}

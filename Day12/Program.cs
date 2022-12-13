using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            Tile.InitMap(Tile.start);
            bool hasArg = args.Length > 0;
            var (x, y) = (Tile.start.x, hasArg ?int.Parse(args[0]):Tile.start.y);
            Tile.Search(Tile.map[x,y], Tile.end);
            Console.WriteLine($"{(!hasArg? "Part 1 Score: ":"")}{Tile.FindPath().Count}");
            
            // not proud of this...
            if (args.Length == 0)
            {
                var a = new List<int>();
                for (int i = 0; i < Tile.map.GetLength(1); i++)
                {
                    var p = new Process();
                    p.StartInfo.FileName = "Day12.exe";
                    p.StartInfo.Arguments = $"{i}";
                    p.StartInfo.RedirectStandardOutput = true;
                    p.Start();
                    a.Add(int.Parse(p.StandardOutput.ReadToEnd().Trim()));
                }
                a.Sort();
                Console.WriteLine($"Part 2 Score: "+ a[0]);
            }
        }
    }
}

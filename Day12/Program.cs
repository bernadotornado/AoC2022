﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using AoC2022;

namespace Day12
{
    class Program
    {
        const int A = 65 - 27;
        const int a = 97 - 1;
        static int EvaluateChar(char c) => (c - (c >= 97 ? a : A))-1;

        class Tile
        {
             public int x;
             public int y;
             int height;
             Tile prev;
             
             public bool wasSearched;
             public static Tile[,] map;
             public static Tile start;
             public static Tile end;
             static Queue<Tile> searched = new Queue<Tile>();
             static Queue<Tile> yetToSearch = new Queue<Tile>();
             static List<Tile> path = new List<Tile>();
             public static List<Tile> lowestElevation = new List<Tile>();

            public static Tile GetTileAt(int x, int y)
            {
                if (x < 0)
                    return null;
                if (x > map.GetLength(0) - 1)
                    return null;
                if (y < 0)
                    return null;
                if (y > map.GetLength(1) - 1)
                    return null;

                return map[x,y];
            }
            
            
            public Tile(int x, int y, int height ,bool isStart, bool isEnd)
            {
                if (isStart)
                    start = this;
                if (isEnd)
                    end = this;
                this.height = height;
                if (height == 0)
                    lowestElevation.Add(this);
                this.x = x;
                this.y = y;
            }

            public List<Tile> GetNeighbours()
            {
                var s= new List<Tile>()
                {
                    GetTileAt(x, y - 1),
                    GetTileAt(x, y + 1),
                    GetTileAt(x - 1, y),
                    GetTileAt(x + 1, y),
                };
                s.RemoveAll(i => i == null);
                return s;
            }
            

            public static bool Search(Tile _start, Tile _end)
            {
                yetToSearch.Enqueue(_start);
                while (yetToSearch.Count>0 || !searched.Contains(_end))
                {
                    if (yetToSearch.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        
                   
                    var current = yetToSearch.First();
                    var neighbours = current.GetNeighbours();
                    foreach (var n in neighbours)
                        if (!yetToSearch.Contains(n) && !searched.Contains(n) && n.height - current.height <= 1) {
                            n.prev = current;
                            yetToSearch.Enqueue(n);
                        }
                    searched.Enqueue(yetToSearch.Dequeue());
                    }
                }

                return true;

            }

            public static List<Tile> FindPath(out bool foundPath)
            {
                Tile t = new Tile(end.x, end.y,end.height ,false, false);
                t.prev = end.prev;
                for (int i = 0;; i++)
                {
                    if (i > 9999)
                    {
                        foundPath = false;
                        path.Clear();
                        break;
                    }
                    if (t.prev == null)
                        break;
                    path.Add(t.prev);
                    t = t.prev;
                    i++;
                }

                foundPath = true;
                return path;
            }
        }


        static void Main(string[] args)
        {
            InitMap(Tile.start);
            bool hasArg = args.Length > 0;
            var (x, y) = (Tile.start.x, hasArg ?int.Parse(args[0]):Tile.start.y);
            Tile.Search(Tile.map[x,y], Tile.end);
            Console.WriteLine($"Part {(hasArg? 2 : 1)} Score: {Tile.FindPath(out bool b).Count}");
            if (args.Length == 0)
                for (int i = 0; i < Tile.map.GetLength(1); i++)
                {
                    var p = new Process();
                    p.StartInfo.FileName= "Day12.exe";
                    p.StartInfo.Arguments = $"{i}";
                    p.Start();
                }
                
        }

        private static void InitMap(Tile start)
        {
            var lines = Common.ParseFile("input.txt");

            int width = lines[0].Length;
            int height = lines.Count;
            var map = new Tile[width, height];

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                map[x, y] = lines[y][x] switch
                {
                    'S' => new Tile(x, y, EvaluateChar('a'), true, false),
                    'E' => new Tile(x, y, EvaluateChar('z'), false, true),
                    _ => new Tile(x, y, EvaluateChar(lines[y][x]), false, false),
                };

            Tile.map = map;
           
        }
    }
}

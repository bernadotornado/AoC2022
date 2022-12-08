using System;
using AoC2022;

namespace Day8
{
    class Program
    {
        static int[] score = new int[2] { 0, 0 };
        static void Main(string[] args)
        {
            var lines = Common.ParseFile(@"input.txt");
            int x = lines[0].Length;
            int y = lines.Count;
            int[,] grid = new int[x, y];
            for (int _y = 0; _y < y; _y++)
                for (int _x = 0; _x < x; _x++)
                    grid[_x, _y] = int.Parse (lines[_y][_x].ToString());

            var visibleOnEdge = 2 * x + 2 * (y - 2);
            int visibleInInterior = 0;
            int highestSceneicScore = 0;
            for (int _y = 1; _y < y-1; _y++)
            {
                for (int _x = 1; _x < x-1; _x++)
                {
                    int scenicScore = 0;
                    var selectedTree = grid[_x, _y];
                    bool visibleFromTop = true;
                    bool visibleFromLeft = true;
                    bool visibleFromRight = true;
                    bool visibleFromBottom = true;
                    int amountOfBlockedTreesTop = 0;
                    int amountOfBlockedTreesLeft = 0;
                    int amountOfBlockedTreesRight = 0;
                    int amountOfBlockedTreesBottom = 0;
                    for (int top = _y - 1; top >= 0; top--)
                        visibleFromTop = visibleFromTop && selectedTree > grid[_x, top];
                    for (int bottom = _y + 1; bottom < y; bottom++)
                        visibleFromBottom = visibleFromBottom && selectedTree > grid[_x, bottom];
                    for (int left = _x - 1; left >= 0; left--)
                        visibleFromLeft = visibleFromLeft && selectedTree > grid[left, _y];
                    for (int right = _x + 1; right < x; right++)
                        visibleFromRight = visibleFromRight && selectedTree >grid[right, _y] ;

                    for (int top = _y - 1; top >= 0; top--)
                    {   amountOfBlockedTreesTop++;
                          if ( grid[_x, top] >= selectedTree)
                              break;
                    }
                    for (int bottom = _y + 1; bottom < y; bottom++)
                    {
                        amountOfBlockedTreesBottom++;
                        if (grid[_x, bottom] >= selectedTree)
                            break;
                    }
                    for (int left = _x - 1; left >= 0; left--)
                    {
                        amountOfBlockedTreesLeft++;
                        if (grid[left, _y] >= selectedTree)
                            break;
                    }
                    for (int right = _x + 1; right < x; right++)
                    {
                        amountOfBlockedTreesRight++;
                        if (grid[right, _y] >= selectedTree)
                            break;
                    }
                    scenicScore = amountOfBlockedTreesBottom * amountOfBlockedTreesLeft * amountOfBlockedTreesRight * amountOfBlockedTreesTop;
                    highestSceneicScore = scenicScore > highestSceneicScore ? scenicScore : highestSceneicScore;
                    if (visibleFromBottom || visibleFromLeft || visibleFromTop || visibleFromRight)
                        visibleInInterior++;
                }
            }

            score[0] = visibleInInterior + visibleOnEdge;
            score[1] = highestSceneicScore;
            Console.WriteLine($"Part 1 Score: {score[0]}\n" +
                              $"Part 2 Score: {score[1]}\n");
        }
    }
}

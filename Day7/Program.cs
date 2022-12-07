using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using AoC2022;
using Microsoft.Win32.SafeHandles;

namespace Day7
{
    class Program
    {
        static void Main()
        {
            int updateSize = 30_000_000;
            var lines = Common.ParseFile("input.txt");
            
            Directory root = new Directory("/", 0);
            Directory currentDirectory = root;
            ParseUNIXCommands(lines, currentDirectory, root);
            //root.PrintDir();

            Console.WriteLine($"Part 1 Score: {Directory.GetTotalSizeOfSmallDirectories(root)}\n" +
                              $"Part 2 Score: {Directory.GetDirectoryToDelete(root, updateSize)}");
        }
        private static void ParseUNIXCommands(List<string> lines, Directory currentDirectory, Directory root)
        {
            foreach (var item in lines)
            {
                 var args = item.Split(" ");
                 if (args[0] != "$" && args[0] != "dir")
                     currentDirectory.filesInDirectory.Add(new File(args[1], int.Parse(args[0])));
                 else if (args[1] == "cd")
                     if (args[2] != "/" && args[2] != "..")
                     {
                         var nextDir = new Directory(args[2], currentDirectory.depth + 1);
                         currentDirectory.subDirectories.Add(nextDir);
                         nextDir.parent = currentDirectory;
                         currentDirectory = nextDir;
                     }
                     else
                         currentDirectory = args[2] == "/" ? root : currentDirectory.parent;
            }
        }
    }
}

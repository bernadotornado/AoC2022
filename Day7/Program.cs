using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using AoC2022;
using Microsoft.Win32.SafeHandles;

namespace Day7
{

    public class File
    {
        public string name;
        public int fileSize;
        public File(string name, int fileSize)
        {
            this.name = name;
            this.fileSize = fileSize;
        }
    }
    public class Directory
    {
        public const int fileSystemSize = 70_000_000;
        public string name;
        public int depth;
        private int dirSize = -1;
        private int totalSize = -1;

        public int DirSize { get {
                if (dirSize > -1)
                    return dirSize;
                int temp = 0;
                foreach (var file in filesInDirectory)
                    temp += file.fileSize;
                dirSize = temp;
                return temp;
            }
        }
         public int TotalSize { get {
                 if (totalSize > -1)
                     return totalSize;
                 int temp = 0;
                 foreach (var dir in this.subDirectories)
                     temp += dir.TotalSize;
                 temp += this.DirSize;
                 totalSize = temp;
                 return temp;
             }
         }
        public List<File> filesInDirectory = new List<File>();
        public List<Directory> subDirectories = new List<Directory>();
        public Directory parent;
        public Directory(string name, int depth)
        {
            this.name = name;
            this.depth = depth;
        }
        public void PrintDir ()
        {
            Console.Write(name == "/" ? $"Dir: Root, TotalSize: {TotalSize}, Depth: {depth}\n":"");
            foreach (var file in filesInDirectory)
            {
                for (int i = 0; i <= depth; i++)
                    Console.Write("\t");
                Console.Write($"File: {file.name}, Size: {file.fileSize}");
                Console.WriteLine();
            }
            foreach (var dir in subDirectories)
            {
                for (int i = 0; i <= depth; i++)
                    Console.Write("\t");
                Console.Write($"Dir: {dir.name}, Size: {dir.DirSize}, TotalSize: {dir.TotalSize}, Depth: {dir.depth}");
                Console.WriteLine();
                dir.PrintDir(); 
            }
        }
        public static int GetDirectoryToDelete(Directory root, int unusedSpaceNeeded)
        {
            int diff = -1 *(Directory.fileSystemSize - unusedSpaceNeeded - root.TotalSize);
            var dirs = new List<int>();

            void SearchFileSystem(Directory directory)
            {
                foreach (var dir in directory.subDirectories)
                {
                    if (dir.TotalSize >= diff)
                        dirs.Add(dir.TotalSize);
                    SearchFileSystem(dir);
                }
                
            }
            SearchFileSystem(root);
            dirs.Sort();
            return dirs[0];
        }
        
        public static int GetTotalSizeOfSmallDirectories(Directory dir)
        {
            int x = 0;
            if (dir.TotalSize < 100_000)
                x = dir.TotalSize;
            foreach(var child in dir.subDirectories)
                x += GetTotalSizeOfSmallDirectories(child);
            return x;
        }

    }

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
                 {
                     File f = new File(args[1], int.Parse(args[0]));
                     currentDirectory.filesInDirectory.Add(f);
                 }
                 else if (args[1] == "cd")
                    switch (args[2])
                    {
                        case "/":
                            currentDirectory = root;
                            break;
                        case "..":
                            var prevDir = currentDirectory.parent;
                            currentDirectory = prevDir;
                            break;
                        default:
                            var nextDir = new Directory(args[2], currentDirectory.depth + 1);
                            currentDirectory.subDirectories.Add(nextDir);
                            nextDir.parent = currentDirectory;
                            currentDirectory = nextDir;
                            break;
                    }
            }
        }
    }
}

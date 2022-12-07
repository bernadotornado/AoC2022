using System;
using System.Collections.Generic;
using AoC2022;

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
        public string name;
        public int depth;
        public int dirSize = 0;
        public int totalSize = 0;
        public List<File> filesInDirectory = new List<File>();
        public List<Directory> subDirectories = new List<Directory>();
        public Directory parent;
        public Directory(string name, int depth)
        {
            this.name = name;
            this.depth = depth;
        }

        public int CountTotalSize()
        {
            int x = 0;
            foreach (var dir in subDirectories)
            {
                x += dir.CountTotalSize();
            }

            totalSize = dirSize + x;
            return totalSize;
        }
        public void CountDirSize()
        {
            foreach (var file in filesInDirectory)
            {
                dirSize += file.fileSize;
            }
        }
        
        public void PrintDir (){
            for (int i = 0; i < depth; i++)
            {
                Console.Write("\t");
            }
            Console.Write($"Listing Dir: {name}");
            Console.WriteLine();

            
            if (filesInDirectory != null)
            {
                foreach (var file in filesInDirectory)
                {
                    for (int i = 0; i < depth; i++)
                    {
                        Console.Write("\t");
                    }
                    Console.Write($"File: {file.name}, Size: {file.fileSize}");
                    Console.WriteLine();
                }
            }

            if (subDirectories != null)
            {
                foreach (var dir in subDirectories)
                {
                    for (int i = 0; i < depth; i++)
                    {
                        Console.Write("\t");
                    }
                        dir.CountDirSize();
                        Console.Write($"Dir: {dir.name}, Size: {dir.dirSize}, Depth: {dir.depth}");
                        Console.WriteLine();
                        dir.PrintDir();
                }
            }
        }
    }

    
    

    class Program
    {
        static void Main()
        {
            var lines = Common.ParseFile("input.txt");
            // List<string> commands = new List<string>();
            // List<string> dirs = new List<string>();
            // List<string> files = new List<string>();
            
            Directory root = new Directory("/", 0);
            Directory currentDirectory = root;
            foreach (var item in lines)
            {
            
                var args = item.Split(" ");
                switch (args[0])
                {
                    case "$":
                        switch (args[1])
                        {
                            case "cd":
              //                  Console.WriteLine("selected Dir: " + args[2]);
              
                                if (args[2] != "/" && args[2] != "..") 
                                {
                                    var nextDir = new Directory(args[2], currentDirectory.depth+1);
                                    currentDirectory.subDirectories.Add(nextDir);
                                    nextDir.parent = currentDirectory;
                                    currentDirectory = nextDir;
                                }

                                if (args[2] == "..")
                                {
                                    var prevDir = currentDirectory.parent;
                                    currentDirectory = prevDir;
                                }
                                break;
                            case "ls":
                        //        Console.WriteLine("ls");
                                break;
                            default:
                                break;
                        }

                        break;
                    case "dir":
                     //   dirs.Add(item);
                        break;
                        
                    default:
                        File f = new File(args[1], int.Parse(args[0]));
                     //   Console.WriteLine($"File: {f.name}, Size: {f.fileSize}");
                        currentDirectory.filesInDirectory.Add(f);
                        break;
                }
            }
            
            root.PrintDir();
            root.CountDirSize();
            Console.WriteLine($"\nTotalSize: {root.CountTotalSize()}");
            Console.WriteLine(lines);
            
            
            
            
            
        }
    }
}

using System.Collections.Generic;
using System;
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
}
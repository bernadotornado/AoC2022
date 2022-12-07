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
        public List<File> filesInDirectory;
        public List<Directory> subDirectories;
        public Directory(string name)
        {
            this.name = name;
        }
    }


    class Program
    {
        static void Main()
        {
            var lines = Common.ParseFile("input.txt");
            List<string> commands = new List<string>();
            List<string> dirs = new List<string>();
            List<string> files = new List<string>();

            Directory root = new Directory("/");

            foreach (var item in lines)
            {
            
             //   List<string> commands = new List<string>();
              //  List<string> commands = new List<string>();
              //var selectedDir = 
                var args = item.Split(" ");
                switch (args[0])
                {
                    case "$":
                        // commands.Add(item);

                        switch (args[1])
                        {
                            case "cd":
                                Console.WriteLine("selected Dir: " + args[2]);
                                break;
                            case "ls":
                                Console.WriteLine("ls");
                                break;
                            default:
                                break;
                        }

                        break;
                    case "dir":
                        dirs.Add(item);
                        break;
                        
                    default:
                        File f = new File(args[1], int.Parse(args[0]));
                        Console.WriteLine($"File: {f.name}, Size: {f.fileSize}");
                        break;
                }
            }
            //foreach (var item in commands)
            //{
            //    Console.WriteLine(item);
            //}
            //foreach (var item in dirs)
            //{
            //    Console.WriteLine(item);
            //}
            //foreach (var item in files)
            //{
            //    Console.WriteLine(item);
            //}

        }
    }
}

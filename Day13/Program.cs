using System;
using System.Collections.Generic;
using AoC2022;

namespace Day13
{
    class Program
    {

        static void ComparePackages(List<string> p)
        {
            var c = p[0].Split("]");
            foreach (var item in c)
            {
                Console.WriteLine(item);
            }
        }
        static void Main(string[] args)
        {
            var lines = Common.ParseFile("input.txt");
            var package = new List<string>();
            foreach (var item in lines)
            {
                if (item == "")
                {
                    ComparePackages(package);
                    package.Clear();
                }
                    package.Add(item);
            }
        }
    }
}

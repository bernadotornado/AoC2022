using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
namespace AoC2022
{
   public class Common
   {
        static void Main(string[] args)
        {
            
        }

        public static List<string> ParseFile(string path)
        {
            StreamReader sr = new StreamReader(@"..\..\..\"+path);
            List<string> s = new List<string>();
            string line;

            while((line = sr.ReadLine()) != null)
            {
                s.Add(line);
            }
            return s;
        }

   }
}

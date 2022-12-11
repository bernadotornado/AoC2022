using System;
using System.Collections.Generic;
using AoC2022;

namespace Day11
{

    class Monkey
    {
        private int id;
        private List<int> startingItems = new List<int>();
        public int testDivisibility;
        private int ifTrueMonkeyID;
        private int ifFalseMonkeyID;
        private string operation;
        public int inspectedItems;
        public static Dictionary<int, Monkey> monkeyRegister= new Dictionary<int, Monkey>();
        public Monkey(List<string> definition)
        {
            id = int.Parse(definition[0].Split(" ")[1][0].ToString());
            monkeyRegister.Add(id, this);
            var a = definition[1].Split("items: ");
            var b = a[1].Replace(",", "");
            var c = b.Split(" ");
             foreach (var d in c)
                startingItems.Add(int.Parse( d.Trim()));
            testDivisibility = int.Parse(definition[3].Split("by ")[1].Trim());
            ifTrueMonkeyID = int.Parse(definition[4].Split("monkey ")[1].Trim());
            ifFalseMonkeyID = int.Parse(definition[5].Split("monkey ")[1].Trim());
            operation = definition[2].Split("= ")[1];
        }

        public Monkey GetMonkeyByID(int id)=> monkeyRegister[id];

        public void RunOperation()
        {
            int firstParam = Human.worryLevel;
            int secondParam;
            var args = operation.Split(" ");
            if (args[2] == "old")
                secondParam = Human.worryLevel;
            else
                secondParam = int.Parse(args[2].Trim());

            switch (args[1])
            {
                case "*": Human.worryLevel = firstParam * secondParam; break;
                case "+": Human.worryLevel = firstParam + secondParam; break;
            }

        }

        public void InspectItems()
        {
            
        }
        
        public override string ToString()
        {
            string s = $"Monkey {id}:\n" +
                       $"\tStarting items: ";
            foreach (var item in startingItems)
                s += $"{item}, ";
            s += $"\n\tOperation: new = {operation}" +
                 $"\n\tTest: divisible by {testDivisibility}" +
                 $"\n\t\tIf true: throw to monkey {ifTrueMonkeyID}" +
                 $"\n\t\tIf false: throw to monkey {ifFalseMonkeyID}";
            return s; 
        }
    }
    
    
    
    class Human
    {
        public static int worryLevel;
        static void Main(string[] args)
        {
            var lines = Common.ParseFile("input.txt");
            var current = new List<string>();
            foreach (var line in lines)
            {
                if (line != "")
                {
                    current.Add(line);
                }
                else
                {
                    var m = new Monkey(current);
                    Console.WriteLine(m);
                    current = new List<string>();
                }
            }
        }
    }
}

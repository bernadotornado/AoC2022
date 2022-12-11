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

        public static Monkey GetMonkeyByID(int id)=> monkeyRegister[id];

        public void RunOperation(ref int worryLevel)
        {
            int firstParam = worryLevel;
            int secondParam;
            var args = operation.Split(" ");
            if (args[2] == "old")
                secondParam = worryLevel;
            else
                secondParam = int.Parse(args[2].Trim());

            switch (args[1])
            {
                case "*": worryLevel = firstParam * secondParam; break;
                case "+": worryLevel = firstParam + secondParam; break;
            }

        }

        public void InspectItems()
        {
            int a = startingItems.Count;
            for (int i = 0; i < a; i++)
            {
                var item = startingItems[i < startingItems.Count ? i: startingItems.Count-1];
                var temp = item;
                RunOperation(ref item);
                inspectedItems++;
                item /= 3;
                ThrowItem(temp);
                if (item % testDivisibility == 0)
                    GetMonkeyByID(ifTrueMonkeyID).RecieveItem(item);
                else
                    GetMonkeyByID(ifFalseMonkeyID).RecieveItem(item);
            }
        }

        public void ThrowItem(int item)
        {
            startingItems.Remove(item);
        }
        public void RecieveItem(int item)
        {
            startingItems.Add(item);
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
            var lines = Common.ParseFile("test.txt");
            var current = new List<string>();
            foreach (var line in lines)
            {
                if (line != "")
                    current.Add(line);
                else
                {
                    var m = new Monkey(current);
                //    Console.WriteLine(m);
                    current = new List<string>();
                }
            }

            // Console.WriteLine(Monkey.monkeyRegister);
            for (int j = 0; j < 20; j++)
            {
                for (int i = 0; i < Monkey.monkeyRegister.Count; i++)
                {
                    Monkey m = Monkey.GetMonkeyByID(i);
                    m.InspectItems();
                    // Console.WriteLine(m);
                }
            }

           // List<int> thrown = new List<int>();
             for (int i = 0; i < Monkey.monkeyRegister.Count; i++)
             {
                // thrown.Add(Monkey.GetMonkeyByID(i).inspectedItems);
                Console.WriteLine(Monkey.GetMonkeyByID(i));
             }

            // Console.WriteLine(thrown[thrown.Count-1]*thrown[thrown.Count-2]);
        }
    }
}

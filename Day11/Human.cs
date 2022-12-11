using System;
using System.Numerics;
using System.Collections.Generic;
using AoC2022;

namespace Day11
{

    class Monkey
    {
        private int id;
        public Queue<BigInteger> startingItems = new Queue<BigInteger>();
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
                startingItems.Enqueue(int.Parse( d.Trim()));
            testDivisibility = int.Parse(definition[3].Split("by ")[1].Trim());
            ifTrueMonkeyID = int.Parse(definition[4].Split("monkey ")[1].Trim());
            ifFalseMonkeyID = int.Parse(definition[5].Split("monkey ")[1].Trim());
            operation = definition[2].Split("= ")[1];
        }

        public static Monkey GetMonkeyByID(int id)=> monkeyRegister[id];

        public void RunOperation(ref BigInteger worryLevel)
        {
            BigInteger firstParam = worryLevel;
            BigInteger secondParam;
            var args = operation.Split(" ");
            if (args[2] == "old")
                secondParam = worryLevel;
            else
                secondParam = BigInteger.Parse(args[2]);

            switch (args[1])
            {
                case "*": worryLevel = firstParam * secondParam; break;
                case "+": worryLevel = firstParam + secondParam; break;
            }

        }

        public void InspectItems()
        {
            int a = startingItems.Count;
            while (startingItems.Count>0)
            {
                BigInteger item = startingItems.Dequeue();
                RunOperation(ref item);
                inspectedItems++;
                item /= 3;
                
                if (item % testDivisibility == 0)
                    GetMonkeyByID(ifTrueMonkeyID).RecieveItem(item);
                else
                    GetMonkeyByID(ifFalseMonkeyID).RecieveItem(item);
            }
        }

        public void RecieveItem(BigInteger item) => startingItems.Enqueue(item);
        public override string ToString()
        {
            string s = $"Monkey {id}:\n" +
                       $"\tStarting items: ";
            foreach (var item in startingItems)
                s += $"{item}, ";
            s += $"\n\tOperation: new = {operation}" +
                 $"\n\tTest: divisible by {testDivisibility}" +
                 $"\n\tInspected: {inspectedItems}" +
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
                    current.Add(line);
                else
                {
                    var m = new Monkey(current);
                    current.Clear();
                }
            }

            if (current.Count > 0)
            {
                var m = new Monkey(current);
            }
            DebugRounds();
            for (int round = 0; round < 20; round++)
            {
                for (int i = 0; i < Monkey.monkeyRegister.Count; i++)
                {
                    Monkey m = Monkey.GetMonkeyByID(i);
                    m.InspectItems();
                }
                Console.WriteLine($"\nAfter round {round+1}, the monkeys are holding items with these worry levels:");

                DebugRounds();
                
              Console.WriteLine("");
            }

             List<int> activeMonkeys = new List<int>();
             for (int i = 0; i < Monkey.monkeyRegister.Count; i++)
             {
                activeMonkeys.Add(Monkey.GetMonkeyByID(i).inspectedItems);
             }
             activeMonkeys.Sort();
             Console.WriteLine("Part 1 Score: "+activeMonkeys[activeMonkeys.Count-1]*activeMonkeys[activeMonkeys.Count-2]);
        }

        private static void DebugRounds()
        {
            for (int i = 0; i < Monkey.monkeyRegister.Count; i++)
            {
                Monkey m = Monkey.GetMonkeyByID(i);
                Console.Write($"Monkey {i}: ");
                foreach (var df in m.startingItems)
                {
                    Console.Write($"{df}, ");
                }

                Console.WriteLine();
            }
        }
    }
}

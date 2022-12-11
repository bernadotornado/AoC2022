using System.Collections.Generic;
using System.Numerics;
namespace Day11
{
    class Monkey
    {
        private int id;
        private Queue<BigInteger> startingItems = new Queue<BigInteger>();
        private int testDivisibility;
        private static int commonDivisor = 1;
        private int ifTrueMonkeyID;
        private int ifFalseMonkeyID;
        private string operation;
        private BigInteger inspectedItems;
        private static List<Monkey> monkeyRegister = new List<Monkey>();
        private Monkey(List<string> definition)
        {
            id = int.Parse(definition[0].Split(" ")[1][0].ToString());
            monkeyRegister.Add(this);
            var a = definition[1].Split("items: ");
            var b = a[1].Replace(",", "");
            var c = b.Split(" ");
            
             foreach (var d in c)
                startingItems.Enqueue(int.Parse( d.Trim()));
             
            testDivisibility = int.Parse(definition[3].Split("by ")[1].Trim());
            commonDivisor *= testDivisibility;
            ifTrueMonkeyID = int.Parse(definition[4].Split("monkey ")[1].Trim());
            ifFalseMonkeyID = int.Parse(definition[5].Split("monkey ")[1].Trim());
            operation = definition[2].Split("= ")[1];
        }

        private static Monkey GetMonkeyByID(int id)=> monkeyRegister[id];
        private void RunOperation(ref BigInteger worryLevel)
        {
            BigInteger secondParam;
            var args = operation.Split(" ");
            if (args[2] == "old")
                secondParam = worryLevel;
            else
                secondParam = BigInteger.Parse(args[2]);

            switch (args[1])
            {
                case "*": worryLevel *= secondParam; break;
                case "+": worryLevel += secondParam; break;
            }
        }

        private void InspectItems(bool monkeyGetsBored)
        {
            while (startingItems.Count>0)
            {
                BigInteger item = startingItems.Dequeue();
                RunOperation(ref item);
                inspectedItems++;
                if(monkeyGetsBored)
                    item /= 3;
                else
                    item %= commonDivisor;

                GetMonkeyByID(item % testDivisibility == 0 
                                ? ifTrueMonkeyID
                                : ifFalseMonkeyID).ReceiveItem(item);
            }
        }

        public void ReceiveItem(BigInteger item) => startingItems.Enqueue(item);
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
        public static string GetMonkeyBusinessScore(List<string> lines, bool monkeyGetsBored, int rounds)
        {
            // Parse Input & Generate Monkeys
            List<string> monkeyDataBuffer = new List<string>();
            ParseMonkeys(lines, monkeyDataBuffer);
            
            // Calculate Rounds:
            for (int round = 0; round < rounds; round++)
                for (int i = 0; i < monkeyRegister.Count; i++)
                    GetMonkeyByID(i).InspectItems(monkeyGetsBored);
            
            // Get Active Monkeys
            List<BigInteger> activeMonkeys = new List<BigInteger>();
            for (int i = 0; i < monkeyRegister.Count; i++)
                activeMonkeys.Add(GetMonkeyByID(i).inspectedItems);
            activeMonkeys.Sort();
            var monkeyBuisnessScore = activeMonkeys[^1] * activeMonkeys[^2];
            
            // Reset
            monkeyRegister = new List<Monkey>();
            commonDivisor = 1;
            monkeyDataBuffer.Clear();
            return "" +monkeyBuisnessScore;
        }

        private static void ParseMonkeys(List<string> lines, List<string> dataBuffer)
        {
            foreach (var line in lines)
            {
                if (line != "")
                    dataBuffer.Add(line);
                else
                {
                    new Monkey(dataBuffer);
                    dataBuffer.Clear();
                }
            }

            if (dataBuffer.Count > 0)
                new Monkey(dataBuffer);
        }
    }
}
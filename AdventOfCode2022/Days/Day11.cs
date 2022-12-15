using AdventOfCode2022.Helper;
using System.Numerics;

namespace AdventOfCode2022.Days
{
    enum OperationType
    {
        Add,
        Multiply,
        Square
    }

    class Monkey
    {
        public List<long> Items { get; set; } = new List<long>();
        public int DivisibleNumber { get; set; }
        public OperationType OpType { get; set; }
        public long OpValue { get; set; }
        public int TrueThrowTarget { get; set; }
        public int FalseThrowTarget { get; set; }
        public long TotalInspections { get; set; } = 0;

        public long DivValue { get; set; }

        private long RunOperation(long number)
        {
            switch (OpType)
            {
                case OperationType.Add:
                    return number += OpValue;
                case OperationType.Multiply:
                    return number *= OpValue;
                case OperationType.Square:
                    return number * number;
            }
            throw new ArgumentException();
        }

        public void InspectItems(List<Monkey> monkeys)
        {
            while (Items.Any())
            {
                ++TotalInspections;
                var item = Items.First();
                Items.RemoveAt(0);
                long newValue = RunOperation(item) % DivValue;

                if (newValue % DivisibleNumber == 0)
                {
                    monkeys[TrueThrowTarget].Items.Add(newValue);
                }
                else
                {
                    monkeys[FalseThrowTarget].Items.Add(newValue);
                }
            }
        }
    }

    class Day11 : IDay
    {
        public long Run(RunConfig config)
        {

            Console.WriteLine($"Start running {GetType()}");

            List<string> data = DataReader.ReadData(this, config).ToList();

            List<Monkey> monkeys = new List<Monkey>();

            int divValue = 1;
            for (int i = 0; i < data.Count(); i += 7)
            {
                Monkey currentMonkey = new Monkey();
                var items = data[i + 1].Substring(17).Split(",");
                foreach (var num in items)
                {
                    currentMonkey.Items.Add(int.Parse(num));
                }

                var operationString = data[i + 2].Substring(22).Split();
                switch (operationString[1])
                {
                    case "*":
                        if (operationString[2] == "old")
                        {
                            currentMonkey.OpType = OperationType.Square;
                        }
                        else
                        {
                            currentMonkey.OpType = OperationType.Multiply;
                            currentMonkey.OpValue = int.Parse(operationString[2]);
                        }
                        break;
                    case "+":
                        currentMonkey.OpType = OperationType.Add;
                        currentMonkey.OpValue = int.Parse(operationString[2]);
                        break;
                    default:
                        throw new Exception();
                }

                var divLine = data[i + 3].Split();
                currentMonkey.DivisibleNumber = int.Parse(divLine[5]);

                var trueLine = data[i + 4].Split();
                currentMonkey.TrueThrowTarget = int.Parse(trueLine[9]);

                var falseLine = data[i + 5].Split();
                currentMonkey.FalseThrowTarget = int.Parse(falseLine[9]);


                monkeys.Add(currentMonkey);
                divValue *= currentMonkey.DivisibleNumber;
            }
            foreach (var monkey in monkeys)
            {
                monkey.DivValue = divValue;
            }
            

            for (int i = 0; i < 10000; ++i)
            {
                Console.WriteLine(i);
                foreach (var monkey in monkeys)
                {
                    monkey.InspectItems(monkeys);
                }
            }

            var newOrder = monkeys.OrderByDescending(x => x.TotalInspections).ToList();
            long total = newOrder[0].TotalInspections * newOrder[1].TotalInspections;
            return total;
        }
    }
}

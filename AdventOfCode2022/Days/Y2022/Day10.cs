using AdventOfCode.Helper;

namespace AdventOfCode.Days.Y2022
{
    class Day10 : IDay
    {
        public long Run(RunConfig config)
        {
            Console.WriteLine($"Start running {GetType()}");

            IEnumerable<string> data = DataReader.ReadData(this, config);

            List<int> instruction = new List<int>();


            foreach (var item in data)
            {
                if (item.StartsWith("addx"))
                {
                    instruction.Add(int.Parse(item.Split()[1]));
                }
                else
                {
                    instruction.Add(0);
                }

            }
            int value = 1;
            long signalValue = 0;
            List<int> cycleChecks = new List<int>() { 20, 60, 100, 140, 180, 220 };

            for (int cycle = 1; instruction.Any(); ++cycle)
            {
                DrawPixel(cycle, value);
                if (cycleChecks.Contains(cycle))
                {
                    var inc = (cycle * value);
                    signalValue += inc;
                }

                int change = instruction.First();
                instruction.RemoveAt(0);
                if (change == 0)
                {
                    continue;
                }
                else
                {
                    ++cycle;
                    DrawPixel(cycle, value);

                    if (cycleChecks.Contains(cycle))
                    {
                        var inc = (cycle * value);
                        signalValue += inc;
                    }

                    value += change;
                }
            }

            return signalValue;
        }

        private void DrawPixel(int cycle, int value)
        {
            int spritePos = (cycle - 1) % 40;
            if (Math.Abs(spritePos - value) <= 1)
            {
                Console.Write('#');
            }
            else
            {
                Console.Write('.');
            }
            if (cycle % 40 == 0)
            {
                Console.Write("\n");
            }
        }
    }
}

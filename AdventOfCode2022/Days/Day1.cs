using AdventOfCode2022.Helper;

namespace AdventOfCode2022.Days
{
    class Day1 : IDay
    {
        public long Run(RunConfig config)
        {
            
            Console.WriteLine($"Start running {GetType()}");

            IEnumerable<string> data = DataReader.ReadData(this, config);


            List<List<int>> elves = new List<List<int>>();

            List<int> currentElf = new List<int>();
            foreach(var line in data)
            {
                if (line == "\n" || line == "")
                {
                    var temp = new List<int>();
                    temp = currentElf.Select(x => x).ToList();
                    elves.Add(temp);
                    currentElf = new List<int>();
                }
                else {
                    currentElf.Add(int.Parse(line));
                }
            }


            var max = elves.Max(x => x.Sum());

            elves = elves.OrderByDescending(x => x.Sum()).ToList();

            return elves[0].Sum() + elves[1].Sum() + elves[2].Sum();

        }
    }
}

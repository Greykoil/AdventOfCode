using AdventOfCode2022.Helper;

namespace AdventOfCode2022.Days
{
    class Day4 : IDay
    {
        public long Run(RunConfig config)
        {
            
            Console.WriteLine($"Start running {GetType()}");

            IEnumerable<string> data = DataReader.ReadData(this, config);


            List<(int, int, int, int)> sections = new List<(int, int, int, int)>();

            foreach (var line in data) {
                var parts = line.Split(",");
                var first = parts[0].Split("-");
                var second = parts[1].Split("-");

                sections.Add((int.Parse(first[0]), int.Parse(first[1]), int.Parse(second[0]), int.Parse(second[1])));
            }


            int overlapCount = sections.Count(x => SectionsOverlap(x));

            return overlapCount;
        }

        private bool SectionsOverlap((int, int, int, int) x)
        {
            if (x.Item1 <= x.Item3 && x.Item2 >= x.Item3)
            {
                return true;
            }
            if (x.Item3 <= x.Item1 && x.Item4 >= x.Item1)
            {
                return true;
            }

            return false;
        }

        private bool SectionsContained((int, int, int, int) x)
        {
            if (x.Item1 <= x.Item3 && x.Item2 >= x.Item4)
            {
                return true;
            }
            if (x.Item1 >= x.Item3 && x.Item2 <= x.Item4)
            {
                return true;
            }

            return false;
        }
    }
}

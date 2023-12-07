using AdventOfCode.Helper;
using System.Xml;

namespace AdventOfCode.Days.Y2023
{
    internal class MapRange
    {
        public string From { get; set; }
        public string To { get; set; }
        public long SourceStart { get; set; }
        public long DestinationStart { get; set; }
        public long Range { get; set; }
    }


    internal class Day5 : IDay
    {
        public long Run(RunConfig config)
        {

            var data = DataReader.ReadData(this, config).ToList();
            List<long> seeds = new List<long>();
            List<MapRange> ranges = new List<MapRange>();
            // First line is all the seeds
            var seedLine = data[0];
            var parts = seedLine.Trim().Split(" ");
            for (int i = 1; i < parts.Count(); ++i)
            {
                seeds.Add(long.Parse(parts[i]));
            }

            string currentFrom = "";
            string currentTo = "";
            foreach (var line in data.Skip(1))
            {
                if (line == "")
                {
                    continue;
                }
                else if (line.Contains("map:"))
                {
                    var lineParts = line.Trim().Split(" ");
                    var desciptors = lineParts[0].Trim().Split("-");
                    currentFrom = desciptors[0];
                    currentTo = desciptors[2];
                }
                else
                {
                    var nums = line.Split(" ");
                    ranges.Add(new MapRange()
                    {
                        From = currentFrom,
                        To = currentTo,
                        DestinationStart = long.Parse(nums[0]),
                        SourceStart = long.Parse(nums[1]),
                        Range = long.Parse(nums[2])
                    });
                }
            }

            List<long> locations = new List<long>();

            for (int i = 0; i < seeds.Count; i += 2)
            {
                Console.WriteLine("Tick");
                for (long j = seeds[i]; j < seeds[i] + seeds[i + 1]; ++j)
                {
                    string currentName = "seed";
                    long currentValue = j;
                    while (currentName != "location")
                    {
                        var rangeMaps =
                            ranges.Where(x =>
                                x.From == currentName &&
                                x.SourceStart <= currentValue &&
                                x.SourceStart + x.Range > currentValue);

                        if (rangeMaps.Any())
                        {
                            if (rangeMaps.Count() != 1)
                            {
                                throw new ArgumentException("Too many range maps found");
                            }
                            var relevantMap = rangeMaps.First();

                            currentValue = relevantMap.DestinationStart + (currentValue - relevantMap.SourceStart);
                            currentName = relevantMap.To;
                        }
                        else
                        {
                            currentName = ranges.First(x => x.From == currentName).To;
                        }

                    }
                    locations.Add(currentValue);
                }
            }

            return locations.Min();
        }
    }
}

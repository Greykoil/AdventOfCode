using AdventOfCode.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AdventOfCode.Days.Y2023
{
    internal class Day2 : IDay
    {
        public long Run(RunConfig config)
        {
            var data = DataReader.ReadData(this, config);
            int sum = 0;
            foreach (var line in data)
            {
                line.Trim();
                var parts = line.Split(":");
                int gameId = int.Parse(parts[0].Substring(5, parts[0].Length -5));

                var gameSplit = parts[1].Split(";");
                int maxReds = 0;
                int maxGreens = 0;
                int maxBlues = 0;
                foreach (var game in gameSplit)
                {
                    var revealse = game.Split(",");
                    

                    foreach (var colour in revealse)
                    {
                        var bits = colour.Trim().Split(" ");
                        switch (bits[1])
                        {
                            case "red":
                                maxReds = Math.Max(maxReds, int.Parse(bits[0]));
                                break;
                            case "green":
                                maxGreens = Math.Max(maxGreens, int.Parse(bits[0]));
                                break;
                            case "blue":
                                maxBlues = Math.Max(maxBlues, int.Parse(bits[0]));
                                break;
                            default:
                                throw new Exception();
                        }
                    }
                }

                sum += maxReds * maxGreens * maxBlues;
            }
            return sum;
        }
    }
}

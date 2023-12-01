using AdventOfCode.Helper;

namespace AdventOfCode.Days.Y2022
{
    class Region
    {
        public int Height { get; set; }

        public int StepsFromStart { get; set; } = -1;

        public int MinPath { get; set; } = -1;
    }

    class Day12 : IDay
    {
        public long Run(RunConfig config)
        {

            Console.WriteLine($"Start running {GetType()}");

            IEnumerable<string> data = DataReader.ReadData(this, config);

            List<List<Region>> regions = new List<List<Region>>();
            Point startPt = new Point();
            Point endPt = new Point();

            foreach (var line in data)
            {
                List<Region> row = new List<Region>();
                foreach (var cha in line)
                {
                    if (cha == 'S')
                    {
                        row.Add(new Region() { Height = 1 });
                        startPt.X = row.Count();
                        startPt.Y = regions.Count();
                    }
                    else if (cha == 'E')
                    {
                        endPt.X = row.Count();
                        endPt.Y = regions.Count();
                        row.Add(new Region() { Height = 26 });
                    }
                    else
                    {
                        row.Add(new Region() { Height = char.ToUpper(cha) - 64 });
                    }
                }
                regions.Add(row);
            }

            int currentMin = int.MaxValue;
            for (int i = 0; i < regions.Count; ++i)
            {
                for (int j = 0; j < regions[0].Count; ++j)
                {
                    if (regions[i][j].Height == 1)
                    {
                        regions[i][j].StepsFromStart = 0;
                        int path = CalculatePath(regions, endPt);
                        regions[i][j].MinPath = path;
                        if (path < 403)
                        {
                            currentMin = Math.Min(path, currentMin);
                        }

                        ResetGrid(regions);
                    }
                }
            }

            foreach (var item in regions)
            {
                for (int i = 0; i < 10; ++i)
                {
                    if (item[i].MinPath != -1 && item[i].MinPath < 1000)
                    {
                        Console.Write(item[i].MinPath);
                    }
                    else
                    {
                        Console.Write("...");
                    }
                }
                Console.WriteLine();
            }
            return currentMin;
        }

        private void ResetGrid(List<List<Region>> regions)
        {
            foreach (var row in regions)
            {
                foreach (var squae in row)
                {
                    squae.StepsFromStart = -1;
                }
            }
        }

        int CalculatePath(List<List<Region>> regions, Point endPt) 
        { 
            while (regions[endPt.Y][endPt.X].StepsFromStart == -1)
            {
                bool hasChanged = false;
                for (int row = 0; row < regions.Count; ++row)
                {
                    for (int column = 0; column < regions[0].Count; ++column)
                    {
                        if (regions[row][column].StepsFromStart == -1)
                        {
                            if (CheckSquare(regions, row, column))
                            {
                                hasChanged = true;
                            }
                        }
                    }
                }

                if (!hasChanged)
                {
                    return int.MaxValue;
                }
            }

            return regions[endPt.Y][endPt.X].StepsFromStart;
        }

        private void PrintGrid(List<List<Region>> regions)
        {
            foreach (var line in regions)
            {
                foreach (var square in line)
                {
                    if (square.StepsFromStart < 10)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(square.StepsFromStart > -1 ? square.StepsFromStart : ".");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private bool CheckSquare(List<List<Region>> regions, int row, int column)
        {
            Region current = regions[row][column];
            
            if (row > 0 && regions[row - 1][column].StepsFromStart != -1 && regions[row - 1][column].Height >= current.Height - 1)
            {
                current.StepsFromStart = regions[row - 1][column].StepsFromStart + 1;
                return true;
            }

            if (row < regions.Count - 1 && regions[row + 1][column].StepsFromStart != -1 && regions[row + 1][column].Height >= current.Height - 1)
            {
                current.StepsFromStart = regions[row + 1][column].StepsFromStart + 1;
                return true;
            }

            if (column > 0 && regions[row][column - 1].StepsFromStart != -1 && regions[row][column - 1].Height >= current.Height - 1)
            {
                current.StepsFromStart = regions[row][column - 1].StepsFromStart + 1;
                return true;
            }

            if (column < regions[0].Count - 1 && regions[row][column + 1].StepsFromStart != -1 && regions[row][column + 1].Height >= current.Height - 1)
            {
                current.StepsFromStart = regions[row][column + 1].StepsFromStart + 1;
                return true;
            }

            return false;
        }
    }
}

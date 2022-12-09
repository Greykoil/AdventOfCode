using AdventOfCode2022.Helper;

namespace AdventOfCode2022.Days
{

    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Day9 : IDay
    {

        public Point HeadPosition { get; set; } = new Point() { X = 0, Y = 0 };
        public List<Point> KnotPositions { get; set; } = new List<Point>();

        List<Point> TailPositionList { get; set; } = new List<Point>();

        public long Run(RunConfig config)
        {

            Console.WriteLine($"Start running {GetType()}");

            IEnumerable<string> data = DataReader.ReadData(this, config);

            int tailCount = 9;
            for (int i = 0; i < tailCount; ++i)
            {
                KnotPositions.Add(new Point() { X = 0, Y = 0 });
            }

            foreach (var line in data)
            {
                MoveHeadPosition(line);
            }

            List<Point> uniquePoints = new List<Point>();
            foreach (var item in TailPositionList)
            {
                if (!uniquePoints.Any(pts => pts.X == item.X && pts.Y == item.Y))
                {
                    uniquePoints.Add(new Point() { X = item.X, Y = item.Y });
                }
            }

            return uniquePoints.Count;
        }

        private void AdjustKnotPositions()
        {
            var lastPosition = HeadPosition;
            foreach (var item in KnotPositions)
            {
                MoveKnot(lastPosition, item);
                lastPosition = item;
            }
            
            TailPositionList.Add(new Point() { X = KnotPositions.Last().X, Y = KnotPositions.Last().Y });
        }

        private void MoveKnot(Point nextKnot, Point currentKnot)
        {
            int xOffset = nextKnot.X - currentKnot.X;
            int yOffset = nextKnot.Y - currentKnot.Y;

            if (xOffset >= 2 || xOffset <= -2 || yOffset >= 2 || yOffset <= -2)
            {
                if (xOffset < 0)
                {
                    currentKnot.X -= 1;
                }
                else if (xOffset > 0)
                {
                    currentKnot.X += 1;
                }
                if (yOffset < 0)
                {
                    currentKnot.Y -= 1;
                }
                else if (yOffset > 0)
                {
                    currentKnot.Y += 1;
                }
            }
        }

        private void MoveHeadPosition(string line)
        {
            var parts = line.Split();
            int distance = int.Parse(parts[1]);
            switch (parts[0])
            {
                case "R":
                    for (int i = 0; i < distance; ++i)
                    {
                        ++HeadPosition.X;
                        AdjustKnotPositions();
                    }
                    return;
                case "D":
                    for (int i = 0; i < distance; ++i)
                    {
                        --HeadPosition.Y;
                        AdjustKnotPositions();
                    }
                    return;
                case "L":
                    for (int i = 0; i < distance; ++i)
                    {
                        --HeadPosition.X;
                        AdjustKnotPositions();
                    }
                    return;
                case "U":
                    for (int i = 0; i < distance; ++i)
                    {
                        ++HeadPosition.Y;
                        AdjustKnotPositions();
                    }
                    return;
            }
        }
    }
}

using AdventOfCode2022.Helper;

namespace AdventOfCode2022.Days
{
    class Tree
    {
        public int Height { get; set; }
        public bool Visible { get; set; } = false;
        public int ScenicView { get; set; }
    }

    class Day8 : IDay
    {
        public long Run(RunConfig config)
        {

            Console.WriteLine($"Start running {GetType()}");

            IEnumerable<string> data = DataReader.ReadData(this, config);


            List<List<Tree>> trees = new List<List<Tree>>();

            foreach (var line in data)
            {
                List<Tree> row = new List<Tree>();

                foreach (var num in line)
                {
                    row.Add(new Tree
                    {
                        Height = int.Parse(new[] { num })
                    });
                }
                trees.Add(row);
            }

            for (int i = 0; i < trees.Count; ++i)
            {
                for (int j = 0; j < trees.Count; ++j)
                {
                    trees[i][j].ScenicView = CaculateSceneScore(trees, i, j);
                }
            }


            return trees.Max(x => x.Max(y => y.ScenicView));

            foreach (var row in trees)
            {
                MarkVisable(row);
                row.Reverse();
                MarkVisable(row);
                row.Reverse();
            }

            for (int i = 0; i < trees[0].Count; ++i)
            {
                var column = new List<Tree>();
                foreach (var row in trees)
                {
                    column.Add(row[i]);
                }

                MarkVisable(column);
                column.Reverse();
                MarkVisable(column);
                column.Reverse();
            }

            int count = trees.Sum(x => x.Count(y => y.Visible));

            return count;
        }

        private int CaculateSceneScore(List<List<Tree>> trees, int i, int j)
        {
            int currentHeight = trees[i][j].Height;

            int leftScore = 0;
            for (int left = j - 1; left >= 0; --left)
            {
                ++leftScore;

                if (trees[i][left].Height >= currentHeight)
                {
                    break;
                }
            }
            
            int rightScore = 0;
            for (int right = j + 1; right < trees.Count; ++right)
            {
                ++rightScore;
                
                if (trees[i][right].Height >= currentHeight)
                {
                    break;
                }
            }

            int upScore = 0;
            for (int up = i - 1; up >= 0; --up)
            {
                ++upScore;
                if (trees[up][j].Height >= currentHeight)
                {
                    break;
                }
            }

            int downScore = 0;
            for (int down = i + 1; down < trees.Count; ++down)
            {
                ++downScore;
                if (trees[down][j].Height >= currentHeight)
                {
                    break;
                }
            }

            return upScore * downScore * leftScore * rightScore;
        }

        private void MarkVisable(List<Tree> row)
        {
            int currentMax = int.MinValue;
            foreach (var item in row)
            {
                if (item.Height > currentMax)
                {
                    item.Visible = true;
                    currentMax = item.Height;
                }
            }
        }
    }
}

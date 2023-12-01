using AdventOfCode.Helper;

namespace AdventOfCode.Days.Y2023
{
    internal class Day1 : IDay
    {
        public long Run(RunConfig config)
        {
            var data = DataReader.ReadData(this, config);
            int total = 0;

            foreach (var line in data)
            {
                for (int i = 0; i < line.Length; ++i)
                {
                    var result = IsDigit(line, i);
                    if (result != -1)
                    {
                        total += 10 * result;
                        break;
                    }
                }
                for (int i = line.Length - 1; i >= 0; --i)
                {
                    var result = IsDigit(line, i);
                    if (result != -1)
                    {
                        total += result;
                        break;
                    }
                }
            }
            return total;
        }

        // Return -1 if it isn't a digit
        private int IsDigit(string line, int position)
        {
            if (int.TryParse(line[position].ToString(), out int result))
            {
                return result;
            }

            List<string> numStrings = new List<string>()
            {
                "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
            };

            foreach (var item in numStrings)
            {
                if (position + item.Count() > line.Length)
                {
                    continue;
                }
                var subStr = line.Substring(position, item.Length);
                if (subStr == item)
                {
                    return numStrings.IndexOf(item) + 1;
                }
            }

            return -1;
        }
    }
}
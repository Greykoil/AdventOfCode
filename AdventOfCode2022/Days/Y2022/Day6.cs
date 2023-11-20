using AdventOfCode.Helper;

namespace AdventOfCode.Days.Y2022
{
    class Day6 : IDay
    {
        public long Run(RunConfig config)
        {

            Console.WriteLine($"Start running {GetType()}");

            IEnumerable<string> data = DataReader.ReadData(this, config);

            string input = data.First();

            int len = 14;

            for (int i = 0; i < input.Length; ++i)
            {
                if (IsStart(input, i, len))
                {
                    return i + len;
                }
            }
            throw new Exception("Answer not found");
        }

        private bool IsStart(string input, int start, int length)
        {
            for (int j = 0; j < length; ++j)
            {
                for (int k = 1; k < length - j; ++k)
                {
                    if (input[start + j] == input[start + j + k])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

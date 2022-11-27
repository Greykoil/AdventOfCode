using AdventOfCode2022.Helper;

namespace AdventOfCode2022.Days
{
    class Day4 : IDay
    {
        public long Run(RunConfig config)
        {
            
            Console.WriteLine($"Start running {GetType()}");

            IEnumerable<string> data = DataReader.ReadData(this, config);

            return 0;
        }
    }
}

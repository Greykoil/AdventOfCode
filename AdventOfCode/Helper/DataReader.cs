using AdventOfCode.Days;

namespace AdventOfCode.Helper
{
    internal class DataReader
    {
        internal static IEnumerable<string> ReadData(IDay day, RunConfig config)
        {
            string fileName = day.GetType().Name + "Input.txt";
            string folderName = config.UseSimpleData ? "SampleData" : "RealData";

            string fullName = $"./Data/{config.Year}/" + folderName + "/" + fileName;

            if (!File.Exists(fullName))
            {
                throw new ArgumentException($"Invalid or unknown data file + {fullName}");
            }

            IEnumerable<string> lines = File.ReadAllLines(fullName);
            return lines;
        }
    }
}

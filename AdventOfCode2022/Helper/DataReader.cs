using AdventOfCode2022.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Helper
{
    internal class DataReader
    {
        internal static IEnumerable<string> ReadData(IDay day, RunConfig config)
        {
            string fileName = day.GetType().Name + "Input.txt";
            string folderName = config.UseSimpleData ? "SimpleData" : "Data";

            string fullName = "./" + folderName + "/" + fileName;

            if (!File.Exists(fullName))
            {
                throw new ArgumentException($"Invalid or unknown data file + {fullName}");
            }

            IEnumerable<string> lines = File.ReadAllLines(fullName);
            return lines;
        }
    }
}

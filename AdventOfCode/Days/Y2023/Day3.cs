using AdventOfCode.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days.Y2023
{

    internal class Digit
    {
        int result;
    }

    internal class Day3 : IDay
    {
        public long Run(RunConfig config)
        {
            var data = DataReader.ReadData(this, config).ToList();
            int gearSum = 0;
            for (int i = 0; i < data.Count; ++i)
            {
                for (int j = 0; j < data[i].Count(); ++j)
                {
                    if (data[i][j] == '*')
                    {
                        var adjNums = FindAdjacentNumbers(data, i, j);
                        if (adjNums.Count() == 2)
                        {
                            gearSum += adjNums[0] * adjNums[1];
                        }
                    }
                }
            }

            return gearSum;


            int numberSum = 0;
            // God damn this is actually kind of annoyingly hard.
            for (int i = 0; i < data.Count(); ++i) {

                bool readingNumber = false;
                int startPoint = -1;
                int currentNumber = -1;
                for (int j = 0; j < data[i].Count(); ++j)
                {
                    // Started reading the number keep going till we hit the end?
                    if (!readingNumber && int.TryParse(data[i][j].ToString(), out currentNumber))
                    { 
                        startPoint = j;
                        int k = j + 1;
                        while (k < data[i].Count() && int.TryParse(data[i][k].ToString(), out int nextDigit))
                        {
                            currentNumber *= 10;
                            currentNumber += nextDigit;
                            ++k;
                        }
                        j = k;
                        if (ValidNumber(data, i, startPoint, currentNumber))
                        {
                            numberSum += currentNumber;
                        }
                    }
                }
            }

            return numberSum;
        }

        private List<int> FindAdjacentNumbers(List<string> data, int row, int column)
        {
            List<int> numbers = new List<int>();
            for (int i = row -1; i <= row + 1; ++i)
            {
                if (i < 0 || i >= data.Count())
                {
                    continue;
                }
                for (int j = column - 1; j <= column + 1; ++j)
                {
                    if (int.TryParse(data[i][j].ToString(), out var result))
                    {
                        while (j >= 0 && int.TryParse(data[i][j].ToString(), out var foo))
                        {
                            --j;
                        }
                        ++j;
                        // We are now at the leftmost side of the number, read the whole thing
                        int currentNumber = 0;
                        while (j < data[i].Length && int.TryParse(data[i][j].ToString(), out var boing)) {
                            currentNumber *= 10;
                            currentNumber += boing;
                            ++j;
                        }
                        numbers.Add(currentNumber);

                    }
                }
            }
            return numbers;
        }

        private bool ValidNumber(List<string> data, int row, int column, int currentNumber)
        {

            for (int i = row - 1; i <= row + 1; ++i)
            {
                if (i < 0 || i >= data.Count())
                {
                    continue;
                }
                for (int  j = column - 1; j <= column + currentNumber.ToString().Length; ++j)
                {
                    if (j < 0 || j >= data[i].Length)
                    {
                        continue;
                    }

                    if (data[i][j] != '.' && !int.TryParse(data[i][j].ToString(), out var foo))
                    {
                        return true;
                    }

                }
            }
            return false;
        }
    }
}

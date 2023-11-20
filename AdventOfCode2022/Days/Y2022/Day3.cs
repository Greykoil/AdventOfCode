using AdventOfCode.Helper;

namespace AdventOfCode.Days.Y2022
{
    class Day3 : IDay
    {
        public long Run(RunConfig config)
        {
            
            Console.WriteLine($"Start running {GetType()}");

            List<string> data = DataReader.ReadData(this, config).ToList();

            long prioritySum  = 0;


            for (int i = 0; i < data.Count(); i += 3)
            {

                foreach (var letter in data[i])                
                {
                    if (data[i +1].Contains(letter) && data[i + 2].Contains(letter))
                    {
                        prioritySum += ConvertLetter(letter);
                        break;
                    }
                }
            }

            return prioritySum;
        }

        private long ConvertLetter(char letter)
        {
            int value = 0;
            if (Char.IsUpper(letter))
            {
                value = letter - 64 + 26;
            } 
            else
            {
                value = letter - 96;
            }

            return value;
        }
    }
}

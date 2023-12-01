using AdventOfCode.Helper;

namespace AdventOfCode.Days.Y2022
{

    class ValueString
    {
        public List<object> SubValues { get; set; } = new List<object>();
    }

    enum Order
    {
        Correct, Incorrect, Same
    }

    class Day13 : IDay
    {
        public long Run(RunConfig config)
        {

            Console.WriteLine($"Start running {GetType()}");

            List<string> data = DataReader.ReadData(this, config).ToList();

            List<ValueString> valueStrings = new List<ValueString>();

            foreach (var line in data.Where(x => x.Length != 0))
            {
                string str = line.Substring(1);
                valueStrings.Add(ReadNextValue(ref str));
            }

            var bufferOne = new ValueString()
            {
                SubValues = new List<object>()
                {
                    new ValueString()
                    {
                        SubValues = new List<object>()
                        {
                            2
                        }
                    }
                }
            };

            var bufferTwo = new ValueString()
            {
                SubValues = new List<object>()
                {
                    new ValueString()
                    {
                        SubValues = new List<object>()
                        {
                            6
                        }
                    }
                }
            };

            valueStrings.Add(bufferOne);
            valueStrings.Add(bufferTwo);


            bool swaps = true;

            while (swaps)
            {
                swaps = false;
                for (int i = 0; i < valueStrings.Count - 1; ++i)
                {
                    if (CorrectOrder(valueStrings[i], valueStrings[i + 1]) != Order.Correct)
                    {
                        swaps = true;
                        var temp = valueStrings[i];
                        valueStrings[i] = valueStrings[i + 1];
                        valueStrings[i + 1] = temp;
                    }
                }
            }

            var indexOne = valueStrings.IndexOf(bufferOne) + 1;
            var indexTwo = valueStrings.IndexOf(bufferTwo) + 1;

            return indexOne * indexTwo;

            List<int> validIndicies = new List<int>();
            for (int i = 0; i < valueStrings.Count() / 2; ++i)
            {
                if (CorrectOrder(valueStrings[i * 2], valueStrings[i * 2 + 1]) == Order.Correct)
                {
                    validIndicies.Add(i);
                }
            }

            return validIndicies.Sum(x => x + 1);
        }

        private Order CorrectOrder(ValueString left, ValueString right)
        {
            int leftCount = left.SubValues.Count();
            int rightCount = right.SubValues.Count();

            for (int current = 0; current < leftCount; ++current)
            {
                if (current >= rightCount)
                {
                    return Order.Incorrect;
                }

                var currentLeft = left.SubValues[current];
                var currentRight = right.SubValues[current];
                if (currentLeft is int && currentRight is int)
                {
                    if ((int)currentRight < (int)currentLeft)
                    {
                        return Order.Incorrect;
                    }
                    else if ((int)currentRight > (int)currentLeft)
                    {
                        return Order.Correct;
                    }
                }
                else if (currentLeft is ValueString && currentRight is ValueString)
                {
                    var res = CorrectOrder((ValueString)currentLeft, (ValueString)currentRight);
                    if (res != Order.Same)
                    {
                        return res;
                    }
                }
                else if (currentLeft is int && currentRight is ValueString)
                {
                    var newLeft = new ValueString();
                    newLeft.SubValues.Add((int)currentLeft);
                    var res = CorrectOrder(newLeft, (ValueString)currentRight);
                    if (res != Order.Same)
                    {
                        return res;
                    }
                }
                else if (currentLeft is ValueString && currentRight is int)
                {
                    var newRight = new ValueString();
                    newRight.SubValues.Add((int)currentRight);
                    var res = CorrectOrder((ValueString)currentLeft, newRight);
                    if (res != Order.Same)
                    {
                        return res;
                    }
                }
            }
            if (leftCount < rightCount)
            {
                return Order.Correct;
            }
            else
            {
                return Order.Same;
            }
        }


        ValueString ReadNextValue(ref string input)
        {
            ValueString currentValue = new ValueString();

            while (true)
            {
                char nextChar = input[0];
                input = input.Substring(1);
                switch (nextChar)
                {
                    case ']':
                        return currentValue;
                    case ',':
                        break;
                    case '[':
                        currentValue.SubValues.Add(ReadNextValue(ref input));
                        break;
                    default:
                        if (input[0] == '0')
                        {
                            // This is a hack to get round values of 10, otherwise all values are single digit.
                            currentValue.SubValues.Add(10);
                        }
                        else
                        {
                            currentValue.SubValues.Add(int.Parse(nextChar.ToString()));
                        }
                        break;
                }
            }

            return currentValue;
        }
    }
}

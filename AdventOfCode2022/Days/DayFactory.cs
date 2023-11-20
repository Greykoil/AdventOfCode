using AdventOfCode.Helper;

namespace AdventOfCode.Days
{
    internal class DayFactory
    {

        internal IDay GenerateDay(RunConfig config)
        {
            Console.WriteLine($"Generating day for {config.Day} - Using {(config.UseSimpleData ? "Simple" : "Full")} data.");

            string dayName = $"AdventOfCode.Days.Y{config.Year}.Day{config.Day}";

            Type? type = Type.GetType(dayName);
            if (type == null)
            {
                throw new ArgumentException($"Invalid day name supplied {config.Day}");
            }

            object instance = Activator.CreateInstance(type);

            return instance as IDay;
        }
    }
}

using AdventOfCode2022.Days;
using AdventOfCode2022.Helper;

RunConfig config = ConfigReader.ReadConfig();

DayFactory dayFactory = new DayFactory();

IDay currentDay = dayFactory.GenerateDay(config);

long answer = currentDay.Run(config);

Console.WriteLine("Answer is");
Console.WriteLine(answer);

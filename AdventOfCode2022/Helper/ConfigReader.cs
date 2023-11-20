using Newtonsoft.Json;

namespace AdventOfCode.Helper
{
    internal struct RunConfig
    {
        public int Day { get; set; }
        public bool UseSimpleData { get; set; }
        public int Year { get; set; }
    }

    internal class ConfigReader
    {
        internal static RunConfig ReadConfig()
        {
            var file = File.ReadAllText("Config.json");
            return JsonConvert.DeserializeObject<RunConfig>(file);
        }
    }
}

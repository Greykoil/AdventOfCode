using Newtonsoft.Json;

namespace AdventOfCode2022.Helper
{
    internal struct RunConfig
    {
        public int Day { get; set; }
        public bool UseSimpleData { get; set; }
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

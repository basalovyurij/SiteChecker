using Newtonsoft.Json;
using System;
using System.IO;

namespace SiteChecker.Core
{
    public class ConfigManager
    {
        private static readonly string _confgiFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.json");

        public static Config Get()
        {
            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(_confgiFileName));
        }

        public static void Save(Config config)
        {
            File.WriteAllText(_confgiFileName, JsonConvert.SerializeObject(config, Formatting.Indented));
        }
    }
}

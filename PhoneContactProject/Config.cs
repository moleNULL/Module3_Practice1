using System.Text.Json;

namespace PhoneContactProject
{
    // class is used to extract CultureInfo from config.json
    internal class Config
    {
        private readonly string _configFile;
        public Config()
        {
            _configFile = "config.json";
        }

        public CultureInfo[] GetCultureInfo()
        {
            string[] cultureNames = GetCultureNames();
            CultureInfo[] cultures = new CultureInfo[cultureNames.Length];

            for (int i = 0; i < cultureNames.Length; i++)
            {
                cultures[i] = new CultureInfo(cultureNames[i]);
            }

            return cultures;
        }

        private string[] GetCultureNames()
        {
            string? jsonString = File.ReadAllText(_configFile);
            var jsonConfig = JsonSerializer.Deserialize<JsonConfig>(jsonString);

            if (jsonConfig is null || jsonConfig.PhoneConfig is null)
            {
                throw new Exception($"Unable to deserialize {_configFile}");
            }

            return jsonConfig.PhoneConfig.Cultures;
        }

        // represents "PhoneConfig" in the JSON config file
        private class JsonConfig
        {
            public PhoneConfig? PhoneConfig { get; set; }
        }

        // represents "Cultures" in the JSON config file
        private class PhoneConfig
        {
            public PhoneConfig()
            {
                Cultures = Array.Empty<string>();
            }

            public string[] Cultures { get; set; }
        }
    }
}

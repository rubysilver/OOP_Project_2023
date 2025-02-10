using System.Text.RegularExpressions;

namespace Data_Layer.Utilities
{
    public static class ConfigUtils
    {
        // Filtered settings
        private static readonly Dictionary<string, string> _settings = new Dictionary<string, string>();
        public static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt");
        private static bool LoadNewSettings = false;
        // Standard settings (schema settings)
        private static Dictionary<string, string> properties = new()
        {
            { "Repository", "API" },
            { "Priority", "Men" },
            { "Language", "en" },
            { "FavoriteMensTeam", "empty" },
            { "FavoriteWomensTeam", "empty" },
            { "FavoriteMensPlayers", "empty" },
            { "FavoriteWomensPlayers", "empty" },
            { "Resolution", "empty" }
        };

        internal static void CreateConfigurationFile()
        {
            try
            {
                if (!File.Exists(filePath) || LoadNewSettings)
                {
                    // LoadNewSettings = false;
                    using (StreamWriter writer = File.CreateText(filePath))
                    {
                        foreach (var property in properties)
                        {
                            writer.WriteLine($"{property.Key}:\"{property.Value}\"");
                        }
                    }
                }
            }
            finally
            {
                InstantiateSettings();
            }
        }
        private static void InstantiateSettings()
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!Regex.IsMatch(line, @"^\w+:""[^""]+""$") || string.IsNullOrWhiteSpace(line))
                        {
                            throw new FormatException();
                        }

                        int index = line.IndexOf(":");
                        if (index != -1)
                        {
                            string key = line.Substring(0, index).Trim();
                            string value = line.Substring(index + 1).Trim().Trim('"');

                            if (!Regex.IsMatch(key, "^[a-zA-Z_]+$") || string.IsNullOrWhiteSpace(value))
                            {
                                throw new FormatException();
                            }


                            _settings[key] = value;
                        }
                    }
                }
            }
            catch 
            {
                _settings.Clear();
                File.Delete(filePath);
                CreateConfigurationFile();
            }
        }
        public static string GetUpdatedSetting(string key) => _settings.ContainsKey(key) ? _settings[key] : string.Empty;
        public static string GetStandardSetting(string key) => properties.ContainsKey(key) ? properties[key] : string.Empty;
        public static Dictionary<string, string> GetSettings() => new Dictionary<string, string>(_settings);
        public static void SetSetting(string key, string value)
        {
            LoadNewSettings = true;
            if (value is not null && value != "")
            {
                properties[key] = value;
            }
        }
    }
}

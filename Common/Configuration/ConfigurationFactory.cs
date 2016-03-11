namespace Common.Configuration
{
    using System;
    using System.IO;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public static class ConfigurationFactory
    {
        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            Converters = new[] { new StringEnumConverter { CamelCaseText = false } }
        };

        public static T FromFile<T>(string bindingsFilePath) where T : Configuration<T>
        {
            if (!File.Exists(bindingsFilePath))
            {
                var defaultConfiguration = Activator.CreateInstance<T>().Default;
                SetupDefaultBindigFile(defaultConfiguration, bindingsFilePath);

                return defaultConfiguration;
            }

            var fileContents = File.ReadAllText(bindingsFilePath);

            var configuration = JsonConvert.DeserializeObject<T>(fileContents, _jsonSerializerSettings);

            if (configuration == null)
            {
                var defaultConfiguration = Activator.CreateInstance<T>().Default;
                SetupDefaultBindigFile(defaultConfiguration, bindingsFilePath);

                return defaultConfiguration;
            }

            return configuration;
        }

        private static void SetupDefaultBindigFile(object configuration, string configFilePath)
        {
            var serializedConfig = JsonConvert.SerializeObject(configuration, _jsonSerializerSettings);

            File.WriteAllText(configFilePath, serializedConfig);
        }
    }
}

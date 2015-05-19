namespace Common
{
    using System.IO;
    using Newtonsoft.Json;

    public static class HotkeyBindingsConfigurationFactory
    {
        public static HotkeyBindingsConfiguration FromFile(string bindingsFilePath)
        {
            if (!File.Exists(bindingsFilePath))
            {
                var hotkeyBindingsConfiguration = new HotkeyBindingsConfiguration();
                SetupDefaultBindigFile(hotkeyBindingsConfiguration, bindingsFilePath);

                return hotkeyBindingsConfiguration;
            }

            var fileContents = File.ReadAllText(bindingsFilePath);

            var configuration = JsonConvert.DeserializeObject<HotkeyBindingsConfiguration>(fileContents);

            if (configuration == null)
            {
                var hotkeyBindingsConfiguration = new HotkeyBindingsConfiguration();
                SetupDefaultBindigFile(hotkeyBindingsConfiguration, bindingsFilePath);

                return hotkeyBindingsConfiguration;
            }

            return configuration;
        }

        private static void SetupDefaultBindigFile(HotkeyBindingsConfiguration hotkeyBindingsConfiguration, string configFilePath)
        {
            var serializedConfig = JsonConvert.SerializeObject(hotkeyBindingsConfiguration);

            File.WriteAllText(configFilePath, serializedConfig);
        }
    }
}
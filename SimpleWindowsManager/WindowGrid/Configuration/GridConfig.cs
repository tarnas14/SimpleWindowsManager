namespace SimpleWindowsManager.WindowGrid.Configuration
{
    using System.Collections.Generic;
    using System.IO;
    using Common;
    using Newtonsoft.Json;

    public class GridConfig
    {
        public IList<Dimensions> GridElements { get; set; }
        public int[][] NeighbourMap { get; set; }
        public int MainElement { get; set; }

        private static GridConfig DummyConfiguration
        {
            get
            {
                var halfOfTheScreen = new Size(960, 540);
                return new GridConfig
                {
                    GridElements = new[]
                    {
                        new Dimensions(new Point(-1440, 270), halfOfTheScreen),
                        new Dimensions(new Point(480, 270), halfOfTheScreen)
                    },
                    NeighbourMap = new[]
                    {
                        new [] { 0, 1, 0, 1 },
                        new [] { 1, 0, 1, 0 }
                    }
                };
            }
        }

        public static GridConfig FromFile(string gridConfigFile)
        {
            if (!File.Exists(gridConfigFile))
            {
                var defaultConfig = DummyConfiguration;
                SetupDefaultGridFile(defaultConfig, gridConfigFile);

                return defaultConfig;
            }

            var fileContents = File.ReadAllText(gridConfigFile);

            var configuration = JsonConvert.DeserializeObject<GridConfig>(fileContents);

            if (configuration == null)
            {
                var defaultConfig = DummyConfiguration;
                SetupDefaultGridFile(defaultConfig, gridConfigFile);

                return defaultConfig;
            }

            return configuration;
        }

        private static void SetupDefaultGridFile(GridConfig defaultGridConfig, string hotkeyBindingsConfigurationFile)
        {
            var serializedConfig = JsonConvert.SerializeObject(defaultGridConfig);

            File.WriteAllText(hotkeyBindingsConfigurationFile, serializedConfig);
        }
    }
}
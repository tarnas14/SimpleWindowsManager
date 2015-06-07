namespace SimpleWindowsManager.WindowGrid.Configuration
{
    using System.Collections.Generic;
    using Common;
    using Common.Configuration;

    public class GridManagerConfig : Configuration<GridManagerConfig>
    {
        public IList<GridConfig> GridConfigurations { get; set; }

        public GridManagerConfig Default
        {
            get
            {
                var halfOfTheScreen = new Size(960, 540);
                var twoScreensExample = new GridConfig
                {
                    Name = "two screens example",
                    GridElements = new[]
                    {
                        new Dimensions(new Point(-1440, 270), halfOfTheScreen),
                        new Dimensions(new Point(480, 270), halfOfTheScreen)
                    },
                    NeighbourMap = new []
                    {
                        new NeighboursMap
                        {
                            Id = 0,
                            Neighbours = new [] { 0, 1, 0, 1 }
                        },
                        new NeighboursMap
                        {
                            Id = 1,
                            Neighbours = new [] { 1, 0, 1, 0 }
                        }
                    }
                };
                var oneScreenExample = new GridConfig
                {
                    Name = "laptop",
                    GridElements = new[]
                    {
                        new Dimensions(new Point(0, 0), new Size(960, 540))
                    }
                };

                return new GridManagerConfig
                {
                    GridConfigurations = new[]
                    {
                        twoScreensExample,
                        oneScreenExample
                    }
                };
            }
        }
    }
}
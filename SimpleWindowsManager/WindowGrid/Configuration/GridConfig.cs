namespace SimpleWindowsManager.WindowGrid.Configuration
{
    using System.Collections.Generic;
    using Common.Configuration;
    using Common;

    public class GridConfig : Configuration<GridConfig>
    {
        public IList<Dimensions> GridElements { get; set; }
        public int[][] NeighbourMap { get; set; }
        public int MainElement { get; set; }

        public GridConfig Default
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
    }
}
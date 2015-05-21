namespace SimpleWindowsManager.WindowGrid.Configuration
{
    using System.Collections.Generic;
    using Common;

    public class GridConfig
    {
        public IList<Dimensions> GridElements { get; set; }
        public int[][] NeighbourMap { get; set; }
        public int MainElement { get; set; }
    }
}
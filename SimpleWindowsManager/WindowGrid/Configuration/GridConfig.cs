namespace SimpleWindowsManager.WindowGrid.Configuration
{
    using System.Collections.Generic;
    using Common;

    public class GridConfig
    {
        public string Name { get; set; }
        public IList<Dimensions> GridElements { get; set; }
        public IList<NeighboursMap> NeighbourMap { get; set; }
        public int MainElement { get; set; }
    }
}
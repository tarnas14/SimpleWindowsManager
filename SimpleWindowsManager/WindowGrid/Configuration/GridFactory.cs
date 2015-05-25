namespace SimpleWindowsManager.WindowGrid.Configuration
{
    using System.Linq;
    using Common;
    using GridSystem;

    public class GridFactory
    {
        public static Grid FromConfig(GridConfig gridConfig)
        {
            var gridElements = gridConfig.GridElements.Select(dimensions => new SquareGridElement(dimensions)).ToList();

            if (gridConfig.NeighbourMap != null)
            {
                for (int i = 0; i < gridElements.Count; i++)
                {
                    var neighbourMap = gridConfig.NeighbourMap[i];
                    var gridElement = gridElements[i];

                    gridElement.SetNeighbour(gridElements[neighbourMap[0]], GridDirections.Up);
                    gridElement.SetNeighbour(gridElements[neighbourMap[1]], GridDirections.Right);
                    gridElement.SetNeighbour(gridElements[neighbourMap[2]], GridDirections.Down);
                    gridElement.SetNeighbour(gridElements[neighbourMap[3]], GridDirections.Left);
                }
            }

            var gridFromConfig = new Grid();
            gridElements.ForEach(gridFromConfig.AddElement);

            return gridFromConfig;
        }
    }
}
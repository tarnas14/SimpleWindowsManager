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
                foreach (var neighboursMap in gridConfig.NeighbourMap)
                {
                    var neighbours = neighboursMap.Neighbours;
                    var gridElement = gridElements[neighboursMap.Id];

                    gridElement.SetNeighbour(gridElements[neighbours[0]], GridDirections.Up);
                    gridElement.SetNeighbour(gridElements[neighbours[1]], GridDirections.Right);
                    gridElement.SetNeighbour(gridElements[neighbours[2]], GridDirections.Down);
                    gridElement.SetNeighbour(gridElements[neighbours[3]], GridDirections.Left);
                }
            }

            var gridFromConfig = new Grid();
            gridElements.ForEach(gridFromConfig.AddElement);

            return gridFromConfig;
        }
    }
}
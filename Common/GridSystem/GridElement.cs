namespace Common.GridSystem
{
    using Windows;

    public class GridElement
    {
        private readonly GridElement[] _neighbours;
        public Dimensions Dimensions { get; private set; }

        public GridElement(Dimensions dimensions)
        {
            _neighbours = new GridElement[4];
            Dimensions = dimensions;
        }

        public void SetNeighbour(GridElement gridElement, GridDirections direction)
        {
            _neighbours[(int)direction] = gridElement;
        }

        public void SetWindow(WindowRepresentation window)
        {
            window.SetDimensions(Dimensions);
        }

        public bool HasWindow(WindowRepresentation window)
        {
            return Dimensions.Equals(window.Dimensions);
        }

        public GridElement GetNeighbour(GridDirections direction)
        {
            return _neighbours[(int)direction];
        }
    }
}
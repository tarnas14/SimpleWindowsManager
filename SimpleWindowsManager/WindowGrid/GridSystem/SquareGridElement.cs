namespace SimpleWindowsManager.WindowGrid.GridSystem
{
    using Common;
    using Common.Windows;

    public class SquareGridElement : GridElement
    {
        private readonly GridElement[] _neighbours;
        public Dimensions Dimensions { get; private set; }

        public SquareGridElement(Dimensions dimensions)
        {
            _neighbours = GetNullObjectNeighbours();
            Dimensions = dimensions;
        }

        private GridElement[] GetNullObjectNeighbours()
        {
            return new GridElement[]
            {
                new NullGridElement(),
                new NullGridElement(),
                new NullGridElement(),
                new NullGridElement()
            };
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
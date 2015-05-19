namespace Common.GridSystem
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Windows;

    public class GridElement
    {
        private readonly GridElement[] _neighbours;
        private readonly ICollection<WindowRepresentation> _windows;
        public Dimensions Dimensions { get; private set; }

        public GridElement(Dimensions dimensions)
        {
            _windows = new Collection<WindowRepresentation>();
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
            _windows.Add(window);
        }

        public bool HasWindow(WindowRepresentation window)
        {
            return _windows.Any(windowInGrid => windowInGrid.Id == window.Id);
        }

        public void Move(WindowRepresentation window, GridDirections direction)
        {
            GetNeighbour(direction).SetWindow(window);
            UnsetWindow(window);
        }

        private void UnsetWindow(WindowRepresentation window)
        {
            var windowToUnset = _windows.First(windowInGrid => windowInGrid.Id == window.Id);

            _windows.Remove(windowToUnset);
        }

        private GridElement GetNeighbour(GridDirections direction)
        {
            return _neighbours[(int)direction];
        }
    }
}
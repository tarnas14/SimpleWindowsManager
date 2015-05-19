namespace Common.GridSystem
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Windows;

    public class Grid
    {
        private GridElement _mainGridElement;
        private readonly ICollection<GridElement> _gridElements;

        public Grid()
        {
            _gridElements = new Collection<GridElement>();
        }

        public void AddElement(GridElement gridElement)
        {
            _gridElements.Add(gridElement);
        }

        public void Move(WindowRepresentation window, GridDirections direction)
        {
            var windowGridElement = GetGridElementWindowIsOn(window);

            if (windowGridElement == null)
            {
                _mainGridElement.SetWindow(window);
                return;
            }

            windowGridElement.Move(window, direction);
        }

        private GridElement GetGridElementWindowIsOn(WindowRepresentation window)
        {
            return _gridElements.FirstOrDefault(gridElement => gridElement.HasWindow(window));
        }

        public void SetAsMain(GridElement gridElement)
        {
            _mainGridElement = gridElement;
        }
    }
}
namespace SimpleWindowsManager.WindowGrid.GridSystem
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Common;
    using Common.Windows;

    public class Grid
    {
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
                var element = FindClosestGridElement(window.Dimensions.Origin, direction);
                element.SetWindow(window);
                return;
            }

            windowGridElement.GetNeighbour(direction).SetWindow(window);
        }

        private GridElement FindClosestGridElement(Point windowOrigin, GridDirections direction)
        {
            IEnumerable<GridElement> elementsInTheDirectionWeAreMoving = new List<GridElement>();

            switch (direction)
            {
                case GridDirections.Left:
                    elementsInTheDirectionWeAreMoving =
                        _gridElements.Where(element => element.Dimensions.Origin.X <= windowOrigin.X);
                    break;
                case GridDirections.Right:
                    elementsInTheDirectionWeAreMoving =
                        _gridElements.Where(element => element.Dimensions.Origin.X >= windowOrigin.X);
                    break;
                case GridDirections.Up:
                    elementsInTheDirectionWeAreMoving =
                        _gridElements.Where(element => element.Dimensions.Origin.Y <= windowOrigin.Y);
                    break;
                case GridDirections.Down:
                    elementsInTheDirectionWeAreMoving =
                        _gridElements.Where(element => element.Dimensions.Origin.Y >= windowOrigin.Y);
                    break;
            }

            var gridElements =
                elementsInTheDirectionWeAreMoving.OrderBy(element => element.Dimensions.Origin.DistanceTo(windowOrigin));

            switch (direction)
            {
                case GridDirections.Left:
                case GridDirections.Right:
                    gridElements = gridElements.ThenBy(element => element.Dimensions.Origin.Y);
                    break;
                case GridDirections.Down:
                case GridDirections.Up:
                    gridElements = gridElements.ThenBy(element => element.Dimensions.Origin.X);
                    break;
            }

            return gridElements.FirstOrDefault() ?? _gridElements.First();
        }

        private GridElement GetGridElementWindowIsOn(WindowRepresentation window)
        {
            return _gridElements.FirstOrDefault(gridElement => gridElement.HasWindow(window));
        }
    }
}
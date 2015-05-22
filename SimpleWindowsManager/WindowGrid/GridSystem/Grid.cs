namespace SimpleWindowsManager.WindowGrid.GridSystem
{
    using System;
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
            var gridElementInLineHorizontally = _gridElements.Where(element => element.Dimensions.Origin.Y == windowOrigin.Y).OrderBy(element => element.Dimensions.Origin.X);

            var gridElementInLinVertically = _gridElements.Where(element => element.Dimensions.Origin.X == windowOrigin.X).OrderBy(element => element.Dimensions.Origin.Y);

            var defaultElement = _gridElements.First();

            switch (direction)
            {
                case GridDirections.Left:
                    return gridElementInLineHorizontally.LastOrDefault(element => element.Dimensions.Origin.X <= windowOrigin.X) ?? defaultElement;
                case GridDirections.Right:
                    return gridElementInLineHorizontally.FirstOrDefault(element => element.Dimensions.Origin.X >= windowOrigin.X) ?? defaultElement;
                case GridDirections.Up:
                    return gridElementInLinVertically.LastOrDefault(element => element.Dimensions.Origin.Y <= windowOrigin.Y) ?? defaultElement;
                case GridDirections.Down:
                    return gridElementInLinVertically.FirstOrDefault(element => element.Dimensions.Origin.Y >= windowOrigin.Y) ?? defaultElement;
                default:
                    return defaultElement;
            }
        }

        private GridElement GetGridElementWindowIsOn(WindowRepresentation window)
        {
            return _gridElements.FirstOrDefault(gridElement => gridElement.HasWindow(window));
        }
    }
}
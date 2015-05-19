namespace Common.GridSystem
{
    using Windows;

    public class Grid
    {
        private GridElement _mainGridElement;

        public void AddElement(GridElement gridElement)
        {
        }

        public void Move(WindowRepresentation windowOutsideGrid, GridDirections direction)
        {
            windowOutsideGrid.SetDimensions(_mainGridElement.Dimensions);
        }

        public void SetAsMain(GridElement gridElement)
        {
            _mainGridElement = gridElement;
        }
    }
}
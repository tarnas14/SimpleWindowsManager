namespace SimpleWindowsManager.WindowGrid.GridSystem
{
    using Common;
    using Common.Windows;

    internal class NullGridElement : GridElement
    {
        public Dimensions Dimensions { get; private set; }
        public void SetNeighbour(GridElement gridElement, GridDirections direction)
        {
            //null object doing nothing
        }

        public void SetWindow(WindowRepresentation window)
        {
            //null object doing nothing
        }

        public bool HasWindow(WindowRepresentation window)
        {
            //null object doing nothing
            return false;
        }

        public GridElement GetNeighbour(GridDirections direction)
        {
            //null object doing nothing
            return new NullGridElement();
        }
    }
}
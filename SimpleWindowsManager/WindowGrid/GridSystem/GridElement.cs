namespace SimpleWindowsManager.WindowGrid.GridSystem
{
    using Common;
    using Common.Windows;

    public interface GridElement
    {
        Dimensions Dimensions { get; }
        void SetNeighbour(GridElement gridElement, GridDirections direction);
        void SetWindow(WindowRepresentation window);
        bool HasWindow(WindowRepresentation window);
        GridElement GetNeighbour(GridDirections direction);
    }
}
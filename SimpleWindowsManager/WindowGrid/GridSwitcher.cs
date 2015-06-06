namespace SimpleWindowsManager.WindowGrid
{
    using System.Collections.Generic;
    using GridSystem;

    public class GridSwitcher
    {
        public GridSwitcher(IList<Grid> grids, WindowsOnGridController windowsOnGridController)
        {
            windowsOnGridController.LoadGrid(grids[0]);
        }
    }
}
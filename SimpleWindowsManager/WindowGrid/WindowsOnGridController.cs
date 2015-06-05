namespace SimpleWindowsManager.WindowGrid
{
    using System.Collections.Generic;
    using Common;
    using Common.Hotkeys;
    using GridSystem;
    using WindowSwitcher;

    public class WindowsOnGridController
    {
        private readonly GridHotkeyConfiguration _hotkeyConfiguration;
        private readonly IList<Grid> _grids;

        public WindowsOnGridController(GridHotkeyConfiguration hotkeyConfiguration, IList<Grid> grids)
        {
            _hotkeyConfiguration = hotkeyConfiguration;
            _grids = grids;
        }

        private static void BindEverything(GridHotkeyConfiguration hotkeyConfiguration, Grid grid)
        {
            hotkeyConfiguration.Left.Enable();
            hotkeyConfiguration.Left.HotkeyPressed += (sender, args) => grid.MoveActiveWindow(GridDirections.Left);

            hotkeyConfiguration.Right.Enable();
            hotkeyConfiguration.Right.HotkeyPressed += (sender, args) => grid.MoveActiveWindow(GridDirections.Right);

            hotkeyConfiguration.Down.Enable();
            hotkeyConfiguration.Down.HotkeyPressed += (sender, args) => grid.MoveActiveWindow(GridDirections.Down);

            hotkeyConfiguration.Up.Enable();
            hotkeyConfiguration.Up.HotkeyPressed += (sender, args) => grid.MoveActiveWindow(GridDirections.Up);
        }

        public void LoadGrid(object sender, GridSelectedEventArgs e)
        {
            BindEverything(_hotkeyConfiguration, _grids[e.Id]);
        }
    }
}
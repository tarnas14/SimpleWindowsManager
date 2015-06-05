namespace SimpleWindowsManager.WindowGrid
{
    using System.Collections.Generic;
    using Common;
    using Common.Hotkeys;
    using Common.Windows;
    using GridSystem;
    using WindowSwitcher;

    public class WindowsOnGridController
    {
        private readonly GridHotkeyConfiguration _hotkeyConfiguration;
        private readonly IList<Grid> _grids;
        private readonly WindowManager _windowManager;

        public WindowsOnGridController(GridHotkeyConfiguration hotkeyConfiguration, IList<Grid> grids, WindowManager windowManager)
        {
            _hotkeyConfiguration = hotkeyConfiguration;
            _grids = grids;
            _windowManager = windowManager;
        }

        private static void BindEverything(GridHotkeyConfiguration hotkeyConfiguration, Grid grid, WindowManager windowManager)
        {
            hotkeyConfiguration.Left.Enable();
            hotkeyConfiguration.Left.HotkeyPressed += (sender, args) =>
            {
                var activeWindow = windowManager.GetActiveWindow();
                grid.Move(activeWindow, GridDirections.Left);
            };

            hotkeyConfiguration.Right.Enable();
            hotkeyConfiguration.Right.HotkeyPressed += (sender, args) =>
            {
                var activeWindow = windowManager.GetActiveWindow();
                grid.Move(activeWindow, GridDirections.Right);
            };

            hotkeyConfiguration.Down.Enable();
            hotkeyConfiguration.Down.HotkeyPressed += (sender, args) =>
            {
                var activeWindow = windowManager.GetActiveWindow();
                grid.Move(activeWindow, GridDirections.Down);
            };

            hotkeyConfiguration.Up.Enable();
            hotkeyConfiguration.Up.HotkeyPressed += (sender, args) =>
            {
                var activeWindow = windowManager.GetActiveWindow();
                grid.Move(activeWindow, GridDirections.Up);
            };
        }

        public void LoadGrid(object sender, GridSelectedEventArgs e)
        {
            BindEverything(_hotkeyConfiguration, _grids[e.Id], _windowManager);
        }
    }
}
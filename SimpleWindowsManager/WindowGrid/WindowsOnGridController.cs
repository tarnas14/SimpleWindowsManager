namespace SimpleWindowsManager.WindowGrid
{
    using Common;
    using Common.Hotkeys;
    using Common.Windows;
    using GridSystem;

    public class WindowsOnGridController
    {
        public WindowsOnGridController(GridHotkeyConfiguration hotkeyConfiguration, Grid grid, WindowManager windowManager)
        {
            BindEverything(hotkeyConfiguration, grid, windowManager);
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
    }
}
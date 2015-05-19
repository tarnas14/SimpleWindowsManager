namespace SimpleWindowsManager.WindowGrid
{
    using System.Collections.Generic;
    using Common;
    using Common.Hotkeys;
    using Common.Windows;
    using GridSystem;

    public class WindowsOnGridController
    {
        public WindowsOnGridController(Dictionary<GridDirections, GlobalHotkey> hotkeyConfiguration, Grid grid, WindowManager windowManager)
        {
            BindEverything(hotkeyConfiguration, grid, windowManager);
        }

        private static void BindEverything(Dictionary<GridDirections, GlobalHotkey> hotkeyConfiguration, Grid grid, WindowManager windowManager)
        {
            foreach (var globalHotkey in hotkeyConfiguration)
            {
                globalHotkey.Value.HotkeyPressed += (sender, args) =>
                {
                    var activeWindow = windowManager.GetActiveWindow();
                    grid.Move(activeWindow, globalHotkey.Key);
                };
            }
        }
    }
}
namespace SimpleWindowsManager.WindowGrid
{
    using Common;
    using Common.Hotkeys;
    using GridSystem;

    public class WindowsOnGridController
    {
        private readonly GridHotkeyConfiguration _hotkeyConfiguration;

        public WindowsOnGridController(GridHotkeyConfiguration hotkeyConfiguration)
        {
            _hotkeyConfiguration = hotkeyConfiguration;
           
        }

        private void BindEverything(Grid grid)
        {
            _hotkeyConfiguration.Left.Enable();
            _hotkeyConfiguration.Left.HotkeyPressed += (sender, args) => grid.MoveActiveWindow(GridDirections.Left);

            _hotkeyConfiguration.Right.Enable();
            _hotkeyConfiguration.Right.HotkeyPressed += (sender, args) => grid.MoveActiveWindow(GridDirections.Right);

            _hotkeyConfiguration.Down.Enable();
            _hotkeyConfiguration.Down.HotkeyPressed += (sender, args) => grid.MoveActiveWindow(GridDirections.Down);

            _hotkeyConfiguration.Up.Enable();
            _hotkeyConfiguration.Up.HotkeyPressed += (sender, args) => grid.MoveActiveWindow(GridDirections.Up);
        }

        public void LoadGrid(Grid grid)
        {
            BindEverything(grid);
        }
    }
}
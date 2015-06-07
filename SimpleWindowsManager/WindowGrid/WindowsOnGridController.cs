namespace SimpleWindowsManager.WindowGrid
{
    using System;
    using Common;
    using Common.Hotkeys;
    using GridSystem;

    public class WindowsOnGridController
    {
        private readonly GridHotkeyConfiguration _hotkeyConfiguration;
        private EventHandler _moveLeft;
        private EventHandler _moveRight;
        private EventHandler _moveUp;
        private EventHandler _moveDown;

        public WindowsOnGridController(GridHotkeyConfiguration hotkeyConfiguration)
        {
            _hotkeyConfiguration = hotkeyConfiguration;
           
        }

        private void BindEverything()
        {
            _hotkeyConfiguration.Left.Enable();
            _hotkeyConfiguration.Left.HotkeyPressed += _moveLeft;

            _hotkeyConfiguration.Right.Enable();
            _hotkeyConfiguration.Right.HotkeyPressed += _moveRight;

            _hotkeyConfiguration.Up.Enable();
            _hotkeyConfiguration.Up.HotkeyPressed += _moveUp;

            _hotkeyConfiguration.Down.Enable();
            _hotkeyConfiguration.Down.HotkeyPressed += _moveDown;
        }

        private void UnbindEverything()
        {
            _hotkeyConfiguration.Left.HotkeyPressed -= _moveLeft;
            _hotkeyConfiguration.Right.HotkeyPressed -= _moveRight;
            _hotkeyConfiguration.Up.HotkeyPressed -= _moveUp;
            _hotkeyConfiguration.Down.HotkeyPressed -= _moveDown;
        }

        public void LoadGrid(Grid grid)
        {
            UnbindEverything();
            SetupEventHandlers(grid);
            BindEverything();
        }

        private void SetupEventHandlers(Grid grid)
        {
            _moveLeft = (sender, args) => grid.MoveActiveWindow(GridDirections.Left);
            _moveRight = (sender, args) => grid.MoveActiveWindow(GridDirections.Right);
            _moveUp = (sender, args) => grid.MoveActiveWindow(GridDirections.Up);
            _moveDown = (sender, args) => grid.MoveActiveWindow(GridDirections.Down);
        }
    }
}
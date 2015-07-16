namespace Common.Hotkeys
{
    using System;

    public class WindowGridHotkeyConfiguration : GridHotkeyConfiguration, IDisposable
    {
        public WindowGridHotkeyConfiguration(ManagedWindowsApiGlobalHotkey left, ManagedWindowsApiGlobalHotkey right, ManagedWindowsApiGlobalHotkey up, ManagedWindowsApiGlobalHotkey down)
        {
            _left = left;
            _right = right;
            _up = up;
            _down = down;
        }
        
        private readonly ManagedWindowsApiGlobalHotkey _left;
        public GlobalHotkey Left { get { return _left; } }

        private readonly ManagedWindowsApiGlobalHotkey _right;
        public GlobalHotkey Right { get { return _right; } }

        private readonly ManagedWindowsApiGlobalHotkey _down;
        public GlobalHotkey Down { get { return _down; } }

        private readonly ManagedWindowsApiGlobalHotkey _up;
        public GlobalHotkey Up { get { return _up; } }
        public void Dispose()
        {
            _left.Dispose();
            _right.Dispose();
            _up.Dispose();
            _down.Dispose();
        }

        public bool Enable()
        {
            return _left.Enable() && _right.Enable() && _down.Enable() && _up.Enable();
        }
    }
}
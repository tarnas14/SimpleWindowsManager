namespace Common.Hotkeys
{
    using System;
    using System.Windows.Forms;
    using ManagedWinapi;

    public class ManagedWindowsApiGlobalHotkey : GlobalHotkey, IDisposable
    {
        private readonly Hotkey _hotkey;

        public ManagedWindowsApiGlobalHotkey()
        {
            _hotkey = new Hotkey();
        }

        public bool Alt
        {
            get { return _hotkey.Alt; }
            set { _hotkey.Alt = value; }
        }

        public bool Ctrl
        {
            get { return _hotkey.Ctrl; }
            set { _hotkey.Ctrl = value; }
        }

        public bool Shift
        {
            get { return _hotkey.Shift; }
            set { _hotkey.Shift = value; }
        }

        public bool WindowsKey
        {
            get { return _hotkey.WindowsKey; }
            set { _hotkey.WindowsKey = value; }
        }

        public Keys KeyCode
        {
            get { return _hotkey.KeyCode; }
            set { _hotkey.KeyCode = value; }
        }

        public void Enable()
        {
            _hotkey.Enabled = true;
        }

        public event EventHandler HotkeyPressed
        {
            add { _hotkey.HotkeyPressed += value; }
            remove { _hotkey.HotkeyPressed -= value; }
        }

        public void Dispose()
        {
            _hotkey.Dispose();
        }
    }
}
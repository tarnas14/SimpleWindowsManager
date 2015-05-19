namespace Common.Hotkeys
{
    using System;
    using System.Windows.Forms;

    public class HotkeyBindingsConfiguration : IDisposable
    {
        public HotkeyBindingsConfiguration()
        {
            WindowSwitcherHotkey = new ManagedWindowsApiGlobalHotkey
            {
                Ctrl = true,
                Shift = true,
                WindowsKey = true,
                KeyCode = Keys.Q
            };

            WindowGridConfiguration = new WindowGridHotkeyConfiguration(
                new ManagedWindowsApiGlobalHotkey {Ctrl = true, Shift = true, WindowsKey = true, KeyCode = Keys.Left},
                new ManagedWindowsApiGlobalHotkey {Ctrl = true, Shift = true, WindowsKey = true, KeyCode = Keys.Right},
                new ManagedWindowsApiGlobalHotkey {Ctrl = true, Shift = true, WindowsKey = true, KeyCode = Keys.Up},
                new ManagedWindowsApiGlobalHotkey {Ctrl = true, Shift = true, WindowsKey = true, KeyCode = Keys.Down});
        }

        public ManagedWindowsApiGlobalHotkey WindowSwitcherHotkey { get; set; }

        public WindowGridHotkeyConfiguration WindowGridConfiguration { get; set; }

        public void Dispose()
        {
            WindowSwitcherHotkey.Dispose();
            WindowGridConfiguration.Dispose();
        }
    }
}
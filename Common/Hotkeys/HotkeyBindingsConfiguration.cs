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

            WindowGridConfiguration = new WindowGridHotkeyConfiguration();
            WindowGridConfiguration.Add(GridDirections.Up, new ManagedWindowsApiGlobalHotkey{Ctrl = true, Shift = true, WindowsKey = true, KeyCode = Keys.Up});
            WindowGridConfiguration.Add(GridDirections.Down, new ManagedWindowsApiGlobalHotkey { Ctrl = true, Shift = true, WindowsKey = true, KeyCode = Keys.Down });
            WindowGridConfiguration.Add(GridDirections.Left, new ManagedWindowsApiGlobalHotkey { Ctrl = true, Shift = true, WindowsKey = true, KeyCode = Keys.Left });
            WindowGridConfiguration.Add(GridDirections.Right, new ManagedWindowsApiGlobalHotkey { Ctrl = true, Shift = true, WindowsKey = true, KeyCode = Keys.Right });
        }

        public ManagedWindowsApiGlobalHotkey WindowSwitcherHotkey { get; set; }

        public WindowGridHotkeyConfiguration WindowGridConfiguration { get; set; }

        public void Dispose()
        {
            WindowSwitcherHotkey.Dispose();
            foreach (var hotkey in WindowGridConfiguration)
            {
                hotkey.Value.Dispose();
            }
        }
    }
}
namespace Common.Hotkeys
{
    using Configuration;
    using System;
    using System.Windows.Forms;

    public class HotkeyBindingsConfiguration : Configuration<HotkeyBindingsConfiguration>, IDisposable
    {
        public ManagedWindowsApiGlobalHotkey WindowSwitcherHotkey { get; set; }

        public WindowGridHotkeyConfiguration WindowGridConfiguration { get; set; }

        public void Dispose()
        {
            WindowSwitcherHotkey.Dispose();
            WindowGridConfiguration.Dispose();
        }

        public HotkeyBindingsConfiguration Default
        {
            get
            {
                return new HotkeyBindingsConfiguration
                {
                    WindowSwitcherHotkey = new ManagedWindowsApiGlobalHotkey
                    {
                        Ctrl = true,
                        Shift = true,
                        WindowsKey = true,
                        KeyCode = Keys.Q
                    },
                    WindowGridConfiguration = new WindowGridHotkeyConfiguration(
                        new ManagedWindowsApiGlobalHotkey { Ctrl = true, Shift = true, WindowsKey = true, KeyCode = Keys.Left },
                        new ManagedWindowsApiGlobalHotkey { Ctrl = true, Shift = true, WindowsKey = true, KeyCode = Keys.Right },
                        new ManagedWindowsApiGlobalHotkey { Ctrl = true, Shift = true, WindowsKey = true, KeyCode = Keys.Up },
                        new ManagedWindowsApiGlobalHotkey { Ctrl = true, Shift = true, WindowsKey = true, KeyCode = Keys.Down }
                    )
                };
            }
        }
    }
}
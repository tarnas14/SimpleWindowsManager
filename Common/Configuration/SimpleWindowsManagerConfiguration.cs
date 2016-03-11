namespace Common.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Hotkeys;

    public class SimpleWindowsManagerConfiguration : Configuration<SimpleWindowsManagerConfiguration>, IDisposable
    {
        public ManagedWindowsApiGlobalHotkey WindowSwitcherHotkey { get; set; }
        public IEnumerable<string> WindowClassNamesToIgnore { get; set; }

        public WindowGridHotkeyConfiguration WindowGridConfiguration { get; set; }

        public void Dispose()
        {
            WindowSwitcherHotkey.Dispose();
            WindowGridConfiguration.Dispose();
        }

        public SimpleWindowsManagerConfiguration Default
        {
            get
            {
                return new SimpleWindowsManagerConfiguration
                {
                    WindowSwitcherHotkey = new ManagedWindowsApiGlobalHotkey
                    {
                        Ctrl = true,
                        Shift = true,
                        WindowsKey = true,
                        KeyCode = Keys.Q
                    },
                    WindowClassNamesToIgnore = new [] { "Windows.UI.Core.CoreWindow", "ApplicationFrameWindow", "Progman" },
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
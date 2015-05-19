namespace Common.Hotkeys
{
    using System.Windows.Forms;

    public class HotkeyBindingsConfiguration
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
        }

        public ManagedWindowsApiGlobalHotkey WindowSwitcherHotkey { set; get; }
    }
}
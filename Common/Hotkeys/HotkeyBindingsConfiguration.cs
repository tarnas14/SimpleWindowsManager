namespace Common.Hotkeys
{
    using System.Windows.Forms;

    public class HotkeyBindingsConfiguration
    {
        public HotkeyBindingsConfiguration()
        {
            WindowSwitcherHotkey = new GlobalHotkey
            {
                Ctrl = true,
                Shift = true,
                WindowsKey = true,
                KeyCode = Keys.Q
            };
        }

        public GlobalHotkey WindowSwitcherHotkey { set; get; }
    }
}
namespace Common
{
    using System.IO;
    using System.Windows.Forms;
    using Newtonsoft.Json;

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
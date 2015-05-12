namespace SimpleWindowsManager
{
    using System;
    using System.Windows.Forms;
    using Common;

    public partial class Switcher : Form
    {
        private readonly GlobalHotkey _switchWindowsGlobalHoteky;

        public Switcher()
        {
            InitializeComponent();
            _switchWindowsGlobalHoteky = new GlobalHotkey
            {
                Shift = true,
                Ctrl = true,
                WindowsKey = true,
                KeyCode = Keys.Q,
                Enabled = true
            };
            _switchWindowsGlobalHoteky.HotkeyPressed += SwitchWindows;
        }

        private void SwitchWindows(object sender, EventArgs e)
        {
            var windows = WindowLister.GetOpenWindows();
            _windowTitles.DataSource = windows;
            Activate();
        }

        public new void Dispose()
        {
            _switchWindowsGlobalHoteky.Dispose();
            base.Dispose();
        }
    }
}

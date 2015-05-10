namespace SimpleWindowsManager
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Common;

    public partial class Switcher : Form
    {
        private const int Modifier = (int)KeyModifiers.Shift | (int)KeyModifiers.Control | (int)KeyModifiers.WinKey;
        private const Keys Key = Keys.Q;

        private readonly GlobalHotkey _switchWindowsGlobalHoteky;
        private readonly NotifyIcon _notifyIcon;

        public Switcher()
        {
            InitializeComponent();
            Load += Switcher_Load;
            Closing += Switcher_Closing;
            _notifyIcon = new NotifyIcon();
            _switchWindowsGlobalHoteky = new GlobalHotkey(Modifier, Key, this);
        }

        private void Switcher_Closing(object sender, CancelEventArgs e)
        {
            _switchWindowsGlobalHoteky.Unregiser();
        }

        private void Switcher_Load(object sender, EventArgs e)
        {
            if (!_switchWindowsGlobalHoteky.Register())
            {
                Console.WriteLine("This hotkey is already registered");
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (GlobalHotkey.IsHotkeyMessage(m) && _switchWindowsGlobalHoteky.HotkeyPressed(m))
            {
                SwitchWindows();
            }

            base.WndProc(ref m);
        }

        private void SwitchWindows()
        {
            Console.WriteLine("handling hotkey");
            Activate();
        }
    }
}

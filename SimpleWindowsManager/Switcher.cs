namespace SimpleWindowsManager
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Common;
    using Properties;

    public partial class Switcher : Form
    {
        private readonly GlobalHotkey _switchWindowsGlobalHoteky;
        private NotifyIcon _notifyIcon;

        public Switcher()
        {
            InitializeComponent();
            InitializeTrayIcon();
            _switchWindowsGlobalHoteky = new GlobalHotkey
            {
                Shift = true,
                Ctrl = true,
                WindowsKey = true,
                KeyCode = Keys.Q,
                Enabled = true
            };
            _switchWindowsGlobalHoteky.HotkeyPressed += SelectWindow;
            _windowTitles.ItemSelected += GoToWindow;
        }

        private void InitializeTrayIcon()
        {
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Visible = true;
            _notifyIcon.Icon = Resources.PlaceholderIcon;
            _notifyIcon.Text = "SimpleWindowsManager";
            _notifyIcon.DoubleClick += BringSwitcherToFront;
        }

        private void BringSwitcherToFront(object sender, EventArgs e)
        {
            ShowSwitcher();
        }

        private void ShowSwitcher()
        {
            Visible = true;
            BringToFront();
            Activate();
        }

        private void GoToWindow(object sender, ElementSelectedEventArgs e)
        {
            var selectedWindow = e.SelectedItem as WindowRepresentation;
            if (selectedWindow == null)
            {
                return;
            }
            selectedWindow.BringToFront();
        }

        private void SelectWindow(object sender, EventArgs e)
        {
            LoadOpenWindows();
            ShowSwitcher();
        }

        private void LoadOpenWindows()
        {
            Task.Run(() =>
            {
                var searchable = new List<ICanBeSearchedFor>();
                var windows = WindowLister.GetOpenWindows();
                searchable.AddRange(windows);
                _windowTitles.Values = searchable;
            });
        }

        public new void Dispose()
        {
            _switchWindowsGlobalHoteky.Dispose();
            _notifyIcon.Dispose();
            base.Dispose();
        }
    }
}

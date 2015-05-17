namespace SimpleWindowsManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Common;
    using Properties;

    public partial class Switcher : Form
    {
        private GlobalHotkey _switchWindowsGlobalHoteky;
        private NotifyIcon _notifyIcon;

        public Switcher()
        {
            InitializeComponent();
            InitializeTrayIcon();
            SetupGlobalHotkey();
            SetupWindowSelection();
        }

        private void InitializeTrayIcon()
        {
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Visible = true;
            _notifyIcon.Icon = Resources.PlaceholderIcon;
            _notifyIcon.Text = "SimpleWindowsManager";
        }

        private void SetupGlobalHotkey()
        {
            _switchWindowsGlobalHoteky = new GlobalHotkey
            {
                Shift = true,
                Ctrl = true,
                WindowsKey = true,
                KeyCode = Keys.Q,
                Enabled = true
            };
            _switchWindowsGlobalHoteky.HotkeyPressed += SelectWindow;
        }

        private void SetupWindowSelection()
        {
            _windowTitles.ItemSelected += GoToWindow;
            UpdateOpenWindowsList();
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
            UpdateOpenWindowsList();
            ShowSwitcher();
        }

        private void UpdateOpenWindowsList()
        {
            Task.Run(() =>
            {
                var searchable = new List<ICanBeSearchedFor>();
                var windows = WindowLister.GetOpenWindows();
                searchable.AddRange(windows);
                searchable.Where(element => _windowTitles.Values.All(window => window.Id != element.Id)).ToList().ForEach(_windowTitles.Values.Add);
                _windowTitles.Values.ToList().RemoveAll(window => searchable.All(element => element.Id != window.Id));
            });
        }

        private void ShowSwitcher()
        {
            Visible = true;
            BringToFront();
            Activate();
        }

        public new void Dispose()
        {
            _switchWindowsGlobalHoteky.Dispose();
            _notifyIcon.Dispose();
            base.Dispose();
        }
    }
}

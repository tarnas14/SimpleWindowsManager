namespace SimpleWindowsManager.WindowSwitcher
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Common;
    using Common.Hotkeys;
    using Common.Windows;
    using Properties;

    public partial class Switcher : Form
    {
        private NotifyIcon _notifyIcon;

        public Switcher(GlobalHotkey switcherHotkey)
        {
            InitializeComponent();
            InitializeTrayIcon();
            SetupGlobalHotkey(switcherHotkey);
            SetupWindowSelection();
            HideWindowFromAltTabList();
        }

        private void HideWindowFromAltTabList()
        {
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void InitializeTrayIcon()
        {
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Visible = true;
            _notifyIcon.Icon = Icon.FromHandle(Resources.TrayIcon.GetHicon());
            _notifyIcon.Text = "SimpleWindowsManager";

            var contextMenu = new ContextMenu();
            var bringToFrontMenuItem = contextMenu.MenuItems.Add("Bring to front");
            var exitMenuItem = contextMenu.MenuItems.Add("Exit");
            _notifyIcon.ContextMenu = contextMenu;

            bringToFrontMenuItem.Click += BringSwitcherToFront;
            exitMenuItem.Click += CloseSwitcher;
        }

        private void CloseSwitcher(object sender, EventArgs e)
        {
            Close();
        }

        private void BringSwitcherToFront(object sender, EventArgs e)
        {
            ShowSwitcher();
        }

        private void SetupGlobalHotkey(GlobalHotkey switcherHotkey)
        {
            switcherHotkey.Enabled = true;
            switcherHotkey.HotkeyPressed += SelectWindow;
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
            _notifyIcon.Dispose();
            base.Dispose();
        }
    }
}

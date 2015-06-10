namespace SimpleWindowsManager.WindowSwitcher
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Common.Hotkeys;
    using Common.Windows;
    using Properties;
    using WindowGrid;

    public partial class Switcher : Form
    {
        private readonly GridSwitcher _gridSwitcher;
        private NotifyIcon _notifyIcon;

        public Switcher(GlobalHotkey switcherHotkey, GridSwitcher gridSwitcher)
        {
            InitializeComponent();
            InitializeTrayIcon();
            SetupGlobalHotkey(switcherHotkey);
            SetupWindowSelection();
            HideWindowFromAltTabList();
            _gridSwitcher = gridSwitcher;
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
            var switchGridMenuItem = contextMenu.MenuItems.Add("Switch grid");
            var exitMenuItem = contextMenu.MenuItems.Add("Exit");
            _notifyIcon.ContextMenu = contextMenu;

            _notifyIcon.DoubleClick += BringSwitcherToFront;
            bringToFrontMenuItem.Click += BringSwitcherToFront;
            switchGridMenuItem.Click += SwitchGrid;
            exitMenuItem.Click += CloseSwitcher;
        }

        private void BringSwitcherToFront(object sender, EventArgs e)
        {
            ShowSwitcher();
        }

        private void SwitchGrid(object sender, EventArgs e)
        {
            _gridSwitcher.ShowDialog();
        }

        private void CloseSwitcher(object sender, EventArgs e)
        {
            Close();
        }

        private void SetupGlobalHotkey(GlobalHotkey switcherHotkey)
        {
            switcherHotkey.Enable();
            switcherHotkey.HotkeyPressed += SelectWindow;
        }

        private void SetupWindowSelection()
        {
            _windowTitles.ItemSelected += GoToWindow;
            UpdateOpenWindowsList();
        }

        private void GoToWindow(object sender, ElementSelectedEventArgs e)
        {
            var selectedWindow = e.SelectedItem as ManagedWindowsApiWindow;
            if (selectedWindow == null)
            {
                return;
            }
            selectedWindow.BringToFront();
            Hide();
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
                AddNewWindowsToList(searchable);
                RemoveUnreachableWindows(searchable);
            });
        }

        private void AddNewWindowsToList(IEnumerable<ICanBeSearchedFor> currentlyAvailableWindows)
        {
            var windowsToAdd =
                currentlyAvailableWindows.Where(
                    availableWindow => !_windowTitles.Values.Any(value => value.Id == availableWindow.Id));
            windowsToAdd.ToList().ForEach(windowToAdd => _windowTitles.Values.Add(windowToAdd));
        }

        private void RemoveUnreachableWindows(IEnumerable<ICanBeSearchedFor> currentlyAvailableWindows)
        {
            var itemsToDelete =
                _windowTitles.Values.Where(
                    window => currentlyAvailableWindows.All(element => element.Id != window.Id) || window.Id == 0);
            itemsToDelete.ToList().ForEach(item => _windowTitles.Values.Remove(item));
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

namespace SimpleWindowsManager
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Common;

    public partial class Switcher : Form
    {
        private readonly WindowLister _windowLister;
        private readonly GlobalHotkey _switchWindowsGlobalHoteky;

        public Switcher(WindowLister windowLister)
        {
            _windowLister = windowLister;
            InitializeComponent();
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
            var searchable = new List<ICanBeSearchedFor>();
            searchable.AddRange(_windowLister.OpenWindows);
            _windowTitles.Values = searchable;
            BringToFront();
            Activate();
        }

        public new void Dispose()
        {
            _switchWindowsGlobalHoteky.Dispose();
            base.Dispose();
        }
    }
}

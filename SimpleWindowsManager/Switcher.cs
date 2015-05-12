namespace SimpleWindowsManager
{
    using System;
    using System.Collections.Generic;
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
            _switchWindowsGlobalHoteky.HotkeyPressed += SelectWindow;
            _windowTitles.ItemSelected += GoToWindow;
        }

        private void GoToWindow(object sender, ElementSelectedEventArgs e)
        {
            var selectedItem = e.SelectedItem as WindowRepresentation;
            throw new NotImplementedException();
        }

        private void SelectWindow(object sender, EventArgs e)
        {
            var searchable = new List<ICanBeSearchedFor>();
            searchable.AddRange(WindowLister.GetOpenWindows());
            _windowTitles.Values = searchable;
            Show();
            Activate();
        }

        public new void Dispose()
        {
            _switchWindowsGlobalHoteky.Dispose();
            base.Dispose();
        }
    }
}

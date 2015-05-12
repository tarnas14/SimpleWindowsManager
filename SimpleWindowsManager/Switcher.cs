namespace SimpleWindowsManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Common;

    public partial class Switcher : Form
    {
        private readonly GlobalHotkey _switchWindowsGlobalHoteky;
        private IList<WindowRepresentation> _windows;

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
            _windowTitles.AutoCompleteMode = AutoCompleteMode.None;
            _windowTitles.KeyUp += SwitcherKeyUpEvent;
        }

        private void SwitcherKeyUpEvent(object sender, KeyEventArgs e)
        {
            if (AcceptedItem(e))
            {
                SwitchToSelectedWindow();
                return;
            }

            AutoCompleteUpdate(e);
        }

        private void AutoCompleteUpdate(KeyEventArgs keyEventArgs)
        {
            if (_windows == null || !IsInput(keyEventArgs) || _windowTitles.Text.Length < 3)
            {
                return;
            }

            var searchExpression = _windowTitles.Text;
            var matchingItems = _windows.Where(window => window.Matches(searchExpression)).ToList();

            while (_windowTitles.Items.Count > 0)
            {
                _windowTitles.Items.RemoveAt(0);
            }

            if (matchingItems.Count == 0)
            {
                return;
            }

            _windowTitles.Items.AddRange(matchingItems.ToArray());
            _windowTitles.SelectedIndex = -1;
            _windowTitles.DroppedDown = true;
        }

        private bool AcceptedItem(KeyEventArgs keyEventArgs)
        {
            return keyEventArgs.KeyCode == Keys.Enter && !keyEventArgs.Alt && !keyEventArgs.Control &&
                   !keyEventArgs.Shift;
        }

        private void SwitchToSelectedWindow()
        {
            var selectedItem = _windowTitles.SelectedItem as WindowRepresentation;
            throw new NotImplementedException();
        }

        private bool IsInput(KeyEventArgs keyEventArgs)
        {
            return 
                !(new List<Keys> {Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.Enter, Keys.Back, Keys.Delete, Keys.Home, Keys.End, Keys.Shift, Keys.ShiftKey, Keys.LShiftKey, Keys.Escape}.Contains(keyEventArgs.KeyCode))
                && !keyEventArgs.Alt && !keyEventArgs.Shift && !keyEventArgs.Control;
        }

        private void SwitchWindows(object sender, EventArgs e)
        {
            _windows = WindowLister.GetOpenWindows();
            Activate();
        }

        public new void Dispose()
        {
            _switchWindowsGlobalHoteky.Dispose();
            base.Dispose();
        }
    }
}

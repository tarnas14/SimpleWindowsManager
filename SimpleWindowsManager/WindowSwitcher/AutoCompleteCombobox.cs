namespace SimpleWindowsManager.WindowSwitcher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Common;
    using Common.Windows;

    public partial class AutoCompleteCombobox : UserControl
    {
        public IList<ICanBeSearchedFor> Values { get; private set; }
        public event EventHandler<ElementSelectedEventArgs> ItemSelected;
        public int AutoCompleteAfterCharacterCount { get; set; }

        public AutoCompleteCombobox()
        {
            InitializeComponent();
            Values = new List<ICanBeSearchedFor>();
            AutoCompleteAfterCharacterCount = 3;
            _comboBox.KeyUp += ComboBoxOnKeyUp;
        }

        private void ComboBoxOnKeyUp(object sender, KeyEventArgs e)
        {
            if (UserSelectedItem(e))
            {
                NotifyAboutItemSelected();
                return;
            }

            if (UserHitTab(e))
            {
                HighlightNextItemOnList();
                return;
            }

            AutoCompleteUpdate(e);
        }

        private bool UserSelectedItem(KeyEventArgs keyEventArgs)
        {
            return keyEventArgs.KeyCode == Keys.Enter && !keyEventArgs.Alt && !keyEventArgs.Control &&
                   !keyEventArgs.Shift;
        }

        private void NotifyAboutItemSelected()
        {
            var selectedItem = _comboBox.SelectedItem as ICanBeSearchedFor;

            if (selectedItem != null && ItemSelected != null)
            {
                ItemSelected(this, new ElementSelectedEventArgs{ SelectedItem = selectedItem });
            }
        }

        private bool UserHitTab(KeyEventArgs keyEventArgs)
        {
            return !keyEventArgs.Alt && !keyEventArgs.Shift && !keyEventArgs.Control && keyEventArgs.KeyCode == Keys.Tab;
        }

        private void HighlightNextItemOnList()
        {
            if (_comboBox.SelectedIndex + 1 == _comboBox.Items.Count)
            {
                _comboBox.SelectedIndex = 0;
                return;
            }

            _comboBox.SelectedIndex++;
        }

        private void AutoCompleteUpdate(KeyEventArgs keyEventArgs)
        {
            if (Values == null || !IsInput(keyEventArgs) || _comboBox.Text.Length < AutoCompleteAfterCharacterCount)
            {
                return;
            }

            _comboBox.DroppedDown = true;
            _comboBox.SelectedIndex = -1;

            var searchExpression = _comboBox.Text;
            var matchingItems = Values.Where(element => element.Matches(searchExpression)).ToList();

            while (_comboBox.Items.Count > 0)
            {
                _comboBox.Items.RemoveAt(0);
            }

            if (matchingItems.Count == 0)
            {
                return;
            }

            _comboBox.Items.AddRange(matchingItems.ToArray());
        }

        private bool IsInput(KeyEventArgs keyEventArgs)
        {
            return
                !(new List<Keys> { Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.Enter, Keys.Delete, Keys.Home, Keys.End, Keys.Shift, Keys.ShiftKey, Keys.LShiftKey, Keys.Escape }.Contains(keyEventArgs.KeyCode))
                && !keyEventArgs.Alt && !keyEventArgs.Shift && !keyEventArgs.Control;
        }
    }
}

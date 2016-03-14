namespace SimpleWindowsManager.WindowSwitcher
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using Common.Windows;

    public partial class AutoCompleteCombobox : UserControl
    {
        private readonly BindingList<ICanBeSearchedFor> _bindigList;
        public IList<ICanBeSearchedFor> Values { get; }
        public event EventHandler<ElementSelectedEventArgs> ItemSelected;
        public int AutoCompleteAfterCharacterCount { get; set; }

        public AutoCompleteCombobox()
        {
            InitializeComponent();
            Values = new List<ICanBeSearchedFor>();
            _bindigList = new BindingList<ICanBeSearchedFor>();
            _comboBox.DataSource = _bindigList;
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

            if (selectedItem != null)
            {
                ItemSelected?.Invoke(this, new ElementSelectedEventArgs{ SelectedItem = selectedItem });
            }
        }

        private bool UserHitTab(KeyEventArgs keyEventArgs)
        {
            return !keyEventArgs.Alt && !keyEventArgs.Shift && !keyEventArgs.Control && keyEventArgs.KeyCode == Keys.Tab;
        }

        private void HighlightNextItemOnList()
        {
            if (_comboBox.Items.Count == 0)
            {
                return;
            }

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

            var searchExpression = _comboBox.Text;
            var matchingItems = Values.Where(element => element.Matches(searchExpression)).ToList();

            UpdateDropdownList(matchingItems, searchExpression);
        }

        private void UpdateDropdownList(List<ICanBeSearchedFor> matchingItems, string searchExpression)
        {
            _comboBox.DroppedDown = true;
            _comboBox.SelectedIndex = -1;

            _bindigList.RaiseListChangedEvents = false;

            _bindigList.Clear();
            matchingItems.ForEach((item) => _bindigList.Add(item));

            _bindigList.RaiseListChangedEvents = true;
            _bindigList.ResetBindings();

            _comboBox.SelectedText = searchExpression;
        }

        private bool IsInput(KeyEventArgs keyEventArgs)
        {
            return
                !(new List<Keys> { Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.Enter, Keys.Delete, Keys.Home, Keys.End, Keys.Shift, Keys.ShiftKey, Keys.LShiftKey, Keys.ControlKey, Keys.LWin, Keys.Escape }.Contains(keyEventArgs.KeyCode))
                && !keyEventArgs.Alt && !keyEventArgs.Shift && !keyEventArgs.Control;
        }
    }
}

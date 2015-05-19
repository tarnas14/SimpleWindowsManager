namespace SimpleWindowsManager.WindowSwitcher
{
    using System;
    using Common;
    using Common.Windows;

    public class ElementSelectedEventArgs : EventArgs
    {
        public ICanBeSearchedFor SelectedItem { get; set; }
    }
}
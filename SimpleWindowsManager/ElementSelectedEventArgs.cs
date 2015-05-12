namespace SimpleWindowsManager
{
    using System;
    using Common;

    public class ElementSelectedEventArgs : EventArgs
    {
        public ICanBeSearchedFor SelectedItem { get; set; }
    }
}
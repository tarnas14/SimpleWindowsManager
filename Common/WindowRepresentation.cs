namespace Common
{
    using System;
    using ManagedWinapi.Windows;

    public class WindowRepresentation
    {
        private readonly SystemWindow _systemWindow;

        public WindowRepresentation(SystemWindow systemWindow)
        {
            _systemWindow = systemWindow;
        }

        public IntPtr MainWindowPointer
        {
            get { return _systemWindow.HWnd; }
        }

        public string Title
        {
            get { return _systemWindow.Title; }
        }

        public string ClassName { get; set; }

        public override string ToString()
        {
            return string.Format("{0}.{1}", _systemWindow.Process.ProcessName, Title);
        }
    }
}
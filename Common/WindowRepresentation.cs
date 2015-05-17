namespace Common
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using ManagedWinapi.Windows;

    public class WindowRepresentation : ICanBeSearchedFor
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

        public bool Matches(string searchExpression)
        {
            return 
                _systemWindow.Process.ProcessName.ToLowerInvariant().Contains(searchExpression.ToLowerInvariant()) || 
                Title.ToLowerInvariant().Contains(searchExpression.ToLowerInvariant());
        }

        public int Id
        {
            get { return _systemWindow.Process.Id; }
        }

        public void BringToFront()
        {
            if (_systemWindow.WindowState == FormWindowState.Minimized)
            {
                ShowWindow(_systemWindow.HWnd, SW_RESTORE);
            }
            else
            {
                BringWindowToTop(_systemWindow.HWnd);
            }
        }

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint Msg);

        [DllImport("user32.dll")]
        private static extern int BringWindowToTop(IntPtr hWnd);

        private const uint SW_RESTORE = 0x09;
    }
}
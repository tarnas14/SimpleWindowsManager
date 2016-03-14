namespace Common.Windows
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using ManagedWinapi.Windows;

    public class ManagedWindowsApiWindow : WindowRepresentation, ICanBeSearchedFor
    {
        private readonly SystemWindow _systemWindow;

        public ManagedWindowsApiWindow(SystemWindow systemWindow)
        {
            _systemWindow = systemWindow;
        }

        public IntPtr MainWindowPointer => _systemWindow.HWnd;

        public string Title => _systemWindow.Title;

        public string ClassName { get; set; }

        public override string ToString()
        {
            return $"{_systemWindow.Process.ProcessName} - {Title}";
        }

        public bool Matches(string searchExpression)
        {
            var expression = searchExpression.ToLowerInvariant();

            var expressions = expression.Split(' ');

            return expressions.All(e => _systemWindow.Process.ProcessName.ToLowerInvariant().Contains(e) || Title.ToLowerInvariant().Contains(e));
        }

        public int Id => _systemWindow.HWnd.ToInt32();

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

        //WindowRepresentation

        public void SetDimensions(Dimensions dimensions)
        {
            if (_systemWindow.WindowState == FormWindowState.Maximized)
            {
                _systemWindow.WindowState = FormWindowState.Normal;
            }

            _systemWindow.Position = new RECT(dimensions.Origin.X, dimensions.Origin.Y, dimensions.Origin.X + dimensions.Size.Width, dimensions.Origin.Y + dimensions.Size.Height);
        }

        public Dimensions Dimensions => new Dimensions(new Point(_systemWindow.Position.Location.X, _systemWindow.Position.Location.Y), new Size(_systemWindow.Position.Size.Width, _systemWindow.Position.Size.Height));
    }
}
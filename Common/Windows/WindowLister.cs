namespace Common.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using ManagedWinapi.Windows;

    public class WindowLister
    {
        private readonly IEnumerable<string> _windowClassNamesToIgnore;

        public WindowLister(IEnumerable<string> windowClassNamesToIgnore)
        {
            _windowClassNamesToIgnore = windowClassNamesToIgnore;
        }

        delegate bool EnumDelegate(IntPtr hWnd, int lParam);

        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowText", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

        public IList<ManagedWindowsApiWindow> GetOpenWindows()
        {
            var allDesktopWindows = new Collection<SystemWindow>();
            EnumDelegate filter = delegate (IntPtr hWnd, int lParam)
            {
                StringBuilder strbTitle = new StringBuilder(255);
                GetWindowText(hWnd, strbTitle, strbTitle.Capacity + 1);
                string strTitle = strbTitle.ToString();

                if (IsWindowVisible(hWnd) && string.IsNullOrEmpty(strTitle) == false)
                {
                    allDesktopWindows.Add(new SystemWindow(hWnd));
                }
                return true;
            };

            EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero);

            var windowsWeCanGetTo = allDesktopWindows.Where(AWindowWeCanGetTo).Select(systemWindow => new ManagedWindowsApiWindow(systemWindow));
            return windowsWeCanGetTo.OrderBy(representation => representation.ToString()).ToList();
        }

        private bool AWindowWeCanGetTo(SystemWindow systemWindow)
        {
            var currentAppId = System.Diagnostics.Process.GetCurrentProcess().Id;

            var windowIsAFolder = systemWindow.Process.ProcessName == "explorer" &&
                                  systemWindow.ClassName == "CabinetWClass";

            var windowIsNotExplorerWindow = systemWindow.Process.ProcessName != "explorer";
            var notSelf = systemWindow.Process.Id != currentAppId;
            var windowIsNotAWindowsApp = !_windowClassNamesToIgnore.Contains(systemWindow.ClassName);

            return
                windowIsAFolder || (
                notSelf &&
                windowIsNotExplorerWindow &&
                windowIsNotAWindowsApp);
        }
    }
}
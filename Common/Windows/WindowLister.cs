namespace Common.Windows
{
    using System.Collections.Generic;
    using System.Linq;
    using ManagedWinapi.Windows;

    public static class WindowLister
    {
        public static IList<ManagedWindowsApiWindow> GetOpenWindows()
        {
            return SystemWindow.AllToplevelWindows.Where(AWindowWeCanGetTo).Select(systemWindow => new ManagedWindowsApiWindow(systemWindow)).OrderBy(representation => representation.ToString()).ToList();
        }

        private static bool AWindowWeCanGetTo(SystemWindow systemWindow)
        {
            return !string.IsNullOrEmpty(systemWindow.Title) && systemWindow.Process.ProcessName != "explorer" && systemWindow.Visible;
        }
    }
}
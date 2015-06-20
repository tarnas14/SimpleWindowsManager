namespace Common.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ManagedWinapi.Windows;

    public static class WindowLister
    {
        public static IList<ManagedWindowsApiWindow> GetOpenWindows()
        {
            var allToplevelWindows = SystemWindow.AllToplevelWindows;
            var windowsWeCanGetTo = allToplevelWindows.Where(AWindowWeCanGetTo).Select(systemWindow => new ManagedWindowsApiWindow(systemWindow));
            return windowsWeCanGetTo.OrderBy(representation => representation.ToString()).ToList();
        }

        private static bool AWindowWeCanGetTo(SystemWindow systemWindow)
        {
            var currentAppId = System.Diagnostics.Process.GetCurrentProcess().Id;

            var windowIsAFolder = systemWindow.Process.ProcessName == "explorer" &&
                                  systemWindow.ClassName == "CabinetWClass";

            var windowHasATitle = !string.IsNullOrWhiteSpace(systemWindow.Title);
            var windowIsNotExplorerWindow = systemWindow.Process.ProcessName != "explorer";
            var notSelf = systemWindow.Process.Id != currentAppId;
            var hasAWindow = systemWindow.Visible;

            return
                windowIsAFolder || (
                hasAWindow &&
                notSelf &&
                windowHasATitle &&
                windowIsNotExplorerWindow);
        }
    }
}
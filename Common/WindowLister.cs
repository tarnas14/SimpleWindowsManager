namespace Common
{
    using System.Collections.Generic;
    using System.Linq;
    using ManagedWinapi.Windows;

    public static class WindowLister
    {
        public static IList<WindowRepresentation> GetOpenWindows()
        {
            return SystemWindow.AllToplevelWindows.Where(AWindowWeCanGetTo).Select(systemWindow => new WindowRepresentation(systemWindow)).OrderBy(representation => representation.Title).ToList();
        }

        private static bool AWindowWeCanGetTo(SystemWindow systemWindow)
        {
            return !string.IsNullOrEmpty(systemWindow.Title) && systemWindow.Process.ProcessName != "explorer" &&
                   systemWindow.Visible;
        }
    }
}
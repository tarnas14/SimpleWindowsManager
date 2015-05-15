namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ManagedWinapi.Hooks;
    using ManagedWinapi.Windows;

    public class WindowLister : IDisposable
    {
        private readonly Hook _hook;

        public WindowLister()
        {
            _hook = new Hook(HookType.WH_SHELL, true, true);
            _hook.Callback += ShellCallback;
            _hook.StartHook();
        }

        private int ShellCallback(int code, IntPtr wparam, IntPtr lparam, ref bool callnext)
        {
            if (code == 1 || code == 2 || code == 4)
            {
                Task.Run(() => {
                    OpenWindows = GetOpenWindows();
                });
            }

            return 0;
        }

        public IList<WindowRepresentation> GetOpenWindows()
        {
            return SystemWindow.AllToplevelWindows.Where(AWindowWeCanGetTo).Select(systemWindow => new WindowRepresentation(systemWindow)).OrderBy(representation => representation.ToString()).ToList();
        }

        private bool AWindowWeCanGetTo(SystemWindow systemWindow)
        {
            return !string.IsNullOrEmpty(systemWindow.Title) && systemWindow.Process.ProcessName != "explorer" &&
                   systemWindow.Visible;
        }

        public IList<WindowRepresentation> OpenWindows { get; private set; }
        public void Dispose()
        {
            _hook.Unhook();
        }
    }
}
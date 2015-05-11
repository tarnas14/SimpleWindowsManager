namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class WindowLister
    {
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool IsWindow(IntPtr hWnd);

        public static IEnumerable<WindowRepresentation> GetOpenWindows()
        {
            IntPtr found = IntPtr.Zero;
            var windows = new List<WindowRepresentation>();

            EnumWindows(delegate(IntPtr wnd, IntPtr param)
            {
                string windowText = GetWindowText(wnd);
                string className = string.Empty;

                if (string.IsNullOrEmpty(windowText) || !IsWindow(wnd))
                {
                    return true;
                }

                windows.Add(new WindowRepresentation
                {
                    Pointer = wnd,
                    Title = windowText,
                    ClassName = className
                });

                return true;
            }, IntPtr.Zero);

            return windows;
        } 

        private static string GetWindowText(IntPtr hWnd)
        {
            int size = GetWindowTextLength(hWnd);
            if (size++ > 0)
            {
                var builder = new StringBuilder(size);
                GetWindowText(hWnd, builder, builder.Capacity);
                return builder.ToString();
            }

            return String.Empty;
        }
    }
}
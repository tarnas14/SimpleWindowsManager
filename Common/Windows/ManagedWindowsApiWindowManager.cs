namespace Common.Windows
{
    using ManagedWinapi.Windows;

    public class ManagedWindowsApiWindowManager : WindowManager
    {
        public WindowRepresentation GetActiveWindow()
        {
            return new ManagedWindowsApiWindow(SystemWindow.ForegroundWindow);
        }
    }
}
namespace Common.Hotkeys
{
    using System;

    public interface GlobalHotkey
    {
        void Enable();
        event EventHandler HotkeyPressed;
    }
}
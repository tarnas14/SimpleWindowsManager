namespace Common.Hotkeys
{
    using System;

    public interface GlobalHotkey
    {
        event EventHandler HotkeyPressed;
    }
}
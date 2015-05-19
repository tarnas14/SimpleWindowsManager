namespace Common.Hotkeys
{
    public interface GridHotkeyConfiguration
    {
        GlobalHotkey Left { get; }
        GlobalHotkey Right { get; }
        GlobalHotkey Down { get; }
        GlobalHotkey Up { get; }
    }
}
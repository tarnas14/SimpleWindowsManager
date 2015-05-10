namespace Common
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class GlobalHotkey
    {
        private const int WM_HOTKEY_MSG_ID = 0x0312;

        private readonly int _modifier;
        private readonly Keys _key;
        private IntPtr _hWnd;
        private readonly int _id;

        public GlobalHotkey(int modifier, Keys key, Form form)
        {
            _modifier = modifier;
            _key = key;
            _hWnd = form.Handle;
            _id = GetHashCode();
        }

        public bool Register()
        {
            return RegisterHotKey(_hWnd, _id, _modifier, (int)_key);
        }

        public bool Unregiser()
        {
            return UnregisterHotKey(_hWnd, _id);
        }

        public override int GetHashCode()
        {
            return _modifier ^ (int)_key ^ _hWnd.ToInt32();
        }

        public bool HotkeyPressed(Message message)
        {
            if (!IsHotkeyMessage(message))
            {
                return false;
            }

            Keys messageKey = (Keys)(((int)message.LParam >> 16) & 0xFFFF);
            var messageModifier =(int)message.LParam & 0xFFFF;
            return messageKey == _key && messageModifier == _modifier;
        }

        public static bool IsHotkeyMessage(Message message)
        {
            return message.Msg == WM_HOTKEY_MSG_ID;
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }
}
using System;
using System.Windows.Forms;

namespace SimpleWindowsManager
{
    class Program
    {
        private NotifyIcon _notifyIcon;

        public Program()
        {
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Visible = true;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var program = new Program();
            program.Run();
        }

        public void Run()
        {
            var mainForm = new Switcher();
            Application.Run(mainForm);
        }
    }
}

namespace SimpleWindowsManager
{
    using System;
    using System.Windows.Forms;
    using Common;

    class Program
    {
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
            var bindingsConfig = HotkeyBindingsConfigurationFactory.FromFile("bindings.json");
            var mainForm = new Switcher(bindingsConfig.WindowSwitcherHotkey);
            Application.Run(mainForm);

            mainForm.Dispose();
            bindingsConfig.WindowSwitcherHotkey.Dispose();
        }
    }
}

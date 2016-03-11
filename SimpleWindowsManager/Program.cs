namespace SimpleWindowsManager
{
    using System;
    using System.Windows.Forms;
    using Common.Configuration;
    using Common.Windows;
    using WindowSwitcher;

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
            var bindingsConfig = ConfigurationFactory.FromFile<SimpleWindowsManagerConfiguration>("bindings.json");

            if (bindingsConfig.WindowSwitcherHotkey.Enable())
            {
                var mainUi = new Switcher(bindingsConfig.WindowSwitcherHotkey, new WindowLister(bindingsConfig.WindowClassNamesToIgnore));

                Application.Run(mainUi);

                mainUi.Dispose();
                bindingsConfig.Dispose();
                return;
            }

            MessageBox.Show("Configured hotkey is already taken, plz reconfigure and start again.", "Hotkey taken",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

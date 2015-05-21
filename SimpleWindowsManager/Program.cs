namespace SimpleWindowsManager
{
    using System;
    using System.Windows.Forms;
    using Common.Hotkeys;
    using Common.Windows;
    using WindowGrid;
    using WindowGrid.Configuration;
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
            var bindingsConfig = HotkeyBindingsConfigurationFactory.FromFile("bindings.json");

            var mainForm = new Switcher(bindingsConfig.WindowSwitcherHotkey);

            new WindowsOnGridController(
                bindingsConfig.WindowGridConfiguration, 
                GridFactory.FromConfig(GridConfig.FromFile("grindConfig.json")),
                new ManagedWindowsApiWindowManager());

            Application.Run(mainForm);

            mainForm.Dispose();
            bindingsConfig.Dispose();
        }
    }
}

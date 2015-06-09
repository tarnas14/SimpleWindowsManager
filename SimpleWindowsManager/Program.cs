namespace SimpleWindowsManager
{
    using System;
    using System.Windows.Forms;
    using Common.Configuration;
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
            var bindingsConfig = ConfigurationFactory.FromFile<HotkeyBindingsConfiguration>("bindings.json");

            var gridManagerConfig = ConfigurationFactory.FromFile<GridManagerConfig>("gridManagerConfig.json");
            var gridFactory = new GridFactory(new ManagedWindowsApiWindowManager());

            var windowsOnGridController = new WindowsOnGridController(
                bindingsConfig.WindowGridConfiguration);

            var mainUi = new Switcher(bindingsConfig.WindowSwitcherHotkey, new GridSwitcher(gridManagerConfig.GridConfigurations, gridFactory, windowsOnGridController));

            Application.Run(mainUi);

            mainUi.Dispose();
            bindingsConfig.Dispose();
        }
    }
}

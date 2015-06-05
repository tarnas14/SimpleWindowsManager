namespace SimpleWindowsManager
{
    using System;
    using System.Linq;
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

            var gridManagerConfig = ConfigurationFactory.FromFile<GridManagerConfig>("grindConfig.json");
            var grids = gridManagerConfig.GridConfigurations.Select(GridFactory.FromConfig).ToList();

            var mainForm = new Switcher(bindingsConfig.WindowSwitcherHotkey, grids);

            var windowsOnGridController = new WindowsOnGridController(
                bindingsConfig.WindowGridConfiguration, 
                grids,
                new ManagedWindowsApiWindowManager());

            mainForm.GridConfigSelected += windowsOnGridController.LoadGrid;

            Application.Run(mainForm);

            mainForm.Dispose();
            bindingsConfig.Dispose();
        }
    }
}

namespace SimpleWindowsManager
{
    using System;
    using System.Windows.Forms;
    using Common;
    using Common.Hotkeys;
    using Common.Windows;
    using WindowGrid;
    using WindowGrid.GridSystem;
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
                GetDummyGrid(),
                new ManagedWindowsApiWindowManager());

            Application.Run(mainForm);

            mainForm.Dispose();
            bindingsConfig.Dispose();
        }

        private Grid GetDummyGrid()
        {
            var grid = new Grid();
            var halfOfTheScreen = new Size(960, 540);
            var leftScreen = new GridElement(new Dimensions(new Point(-1440, 270), halfOfTheScreen ));
            grid.AddElement(leftScreen);
            var rightScreen = new GridElement(new Dimensions(new Point(480, 270), halfOfTheScreen));
            leftScreen.SetNeighbour(rightScreen, GridDirections.Right);
            rightScreen.SetNeighbour(leftScreen, GridDirections.Left);
            grid.AddElement(rightScreen);
            grid.SetAsMain(rightScreen);

            return grid;
        }
    }
}

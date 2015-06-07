namespace SimpleWindowsManager.WindowGrid
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Configuration;
    using GridSystem;

    public partial class GridSwitcher : Form
    {
        private readonly IList<GridConfig> _gridConfigs;
        private readonly GridFactory _gridFactory;
        private readonly WindowsOnGridController _windowsOnGridController;

        public GridSwitcher(IList<GridConfig> gridConfigs, GridFactory gridFactory, WindowsOnGridController windowsOnGridController)
        {
            InitializeComponent();
            
            _gridConfigs = gridConfigs;
            _gridFactory = gridFactory;
            _windowsOnGridController = windowsOnGridController;

            _gridList.SelectedIndexChanged += LoadNewSelectedGrid;

            SetupGridList();
        }

        private void LoadNewSelectedGrid(object sender, EventArgs e)
        {
            LoadGrid(_gridList.SelectedIndex);
            Hide();
        }
        
        private void LoadGrid(int selectedIndex)
        {
            _windowsOnGridController.LoadGrid(GetGrid(selectedIndex));
        }

        private void SetupGridList()
        {
            foreach (var grid in _gridConfigs)
            {
                _gridList.Items.Add(grid);
            }

            const int initialGridIndex = 0;
            _gridList.SelectedIndex = initialGridIndex;
            LoadGrid(initialGridIndex);
        }

        private Grid GetGrid(int gridIndex)
        {
            var grid = _gridFactory.FromConfig(_gridConfigs[gridIndex]);

            return grid;
        }
    }
}

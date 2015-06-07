namespace SimpleWindowsManager.WindowGrid
{
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

            LoadGridList();

            _windowsOnGridController.LoadGrid(GetGrid(0));
        }

        private Grid GetGrid(int gridIndex)
        {
            var grid = _gridFactory.FromConfig(_gridConfigs[gridIndex]);

            return grid;
        }

        private void LoadGridList()
        {
            foreach (var grid in _gridConfigs)
            {
                _gridList.Items.Add(grid);
            }
        }
    }
}

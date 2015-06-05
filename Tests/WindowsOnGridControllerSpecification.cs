namespace Tests
{
    using System.Collections.Generic;
    using Common;
    using Common.Hotkeys;
    using Common.Windows;
    using FakeItEasy;
    using NUnit.Framework;
    using SimpleWindowsManager.WindowGrid;
    using SimpleWindowsManager.WindowGrid.GridSystem;
    using SimpleWindowsManager.WindowSwitcher;

    [TestFixture]
    class WindowsOnGridControllerSpecification
    {
        [Test]
        public void ShouldMoveActiveWindowWhenHotkeyPressed()
        {
            //given
            var activeWindow = A.Fake<WindowRepresentation>();
            var dimensions = new Dimensions(new Point(0 ,0), new Size(100, 100));
            A.CallTo(() => activeWindow.Dimensions).Returns(dimensions);

            var windowManager = A.Fake<WindowManager>();
            A.CallTo(() => windowManager.GetActiveWindow()).Returns(activeWindow);

            var dummyHotkeyConfiguration = A.Fake<GridHotkeyConfiguration>();
            A.CallTo(() => dummyHotkeyConfiguration.Left).Returns(A.Fake<GlobalHotkey>());
            A.CallTo(() => dummyHotkeyConfiguration.Right).Returns(A.Fake<GlobalHotkey>());
            A.CallTo(() => dummyHotkeyConfiguration.Up).Returns(A.Fake<GlobalHotkey>());
            A.CallTo(() => dummyHotkeyConfiguration.Down).Returns(A.Fake<GlobalHotkey>());

            var grid = new Grid(windowManager);
            var gridElement = new SquareGridElement(new Dimensions(new Point(0,0), new Size(1, 1)));
            grid.AddElement(gridElement);
            var grids = new List<Grid>
            {
                grid
            };

            var windowsOnGridController = new WindowsOnGridController(dummyHotkeyConfiguration, grids);
            windowsOnGridController.LoadGrid(this, new GridSelectedEventArgs{Id = 0});

            //when
            dummyHotkeyConfiguration.Left.HotkeyPressed += Raise.WithEmpty();
            dummyHotkeyConfiguration.Right.HotkeyPressed += Raise.WithEmpty();
            dummyHotkeyConfiguration.Up.HotkeyPressed += Raise.WithEmpty();
            dummyHotkeyConfiguration.Down.HotkeyPressed += Raise.WithEmpty();

            //then
            A.CallTo(() => activeWindow.SetDimensions(A<Dimensions>.That.Matches(newDimensions => newDimensions.Equals(gridElement.Dimensions)))).MustHaveHappened(Repeated.Exactly.Times(4));
        }
    }
}

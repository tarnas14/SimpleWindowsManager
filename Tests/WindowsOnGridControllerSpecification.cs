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

    [TestFixture]
    class WindowsOnGridControllerSpecification
    {
        [Test]
        [TestCase(GridDirections.Up)]
        [TestCase(GridDirections.Down)]
        [TestCase(GridDirections.Left)]
        [TestCase(GridDirections.Right)]
        public void ShouldMoveActiveWindowWhenHotkeyPressed(GridDirections direction)
        {
            //given
            var activeWindow = A.Fake<WindowRepresentation>();

            var windowManager = A.Fake<WindowManager>();

            A.CallTo(() => windowManager.GetActiveWindow()).Returns(activeWindow);

            var dummyHotkeyConfiguration = new Dictionary<GridDirections, GlobalHotkey>();
            dummyHotkeyConfiguration.Add(GridDirections.Up, A.Fake<GlobalHotkey>());
            dummyHotkeyConfiguration.Add(GridDirections.Down, A.Fake<GlobalHotkey>());
            dummyHotkeyConfiguration.Add(GridDirections.Left, A.Fake<GlobalHotkey>());
            dummyHotkeyConfiguration.Add(GridDirections.Right, A.Fake<GlobalHotkey>());

            var grid = new Grid();
            var gridElement = new GridElement(new Dimensions(new Point(0,0), new Size(1, 1)));
            grid.AddElement(gridElement);
            grid.SetAsMain(gridElement);

            new WindowsOnGridController(dummyHotkeyConfiguration, grid, windowManager);

            //when
            dummyHotkeyConfiguration[direction].HotkeyPressed += Raise.WithEmpty();

            //then
            A.CallTo(() => activeWindow.SetDimensions(A<Dimensions>.That.Matches(dimensions => dimensions.Equals(gridElement.Dimensions)))).MustHaveHappened();
        }
    }
}

namespace Tests
{
    using Common;
    using Common.Hotkeys;
    using Common.Windows;
    using FakeItEasy;
    using NUnit.Framework;
    using SimpleWindowsManager.WindowGrid;
    using SimpleWindowsManager.WindowGrid.Configuration;

    public class GridSwitcherSpecification
    {
        [Test]
        public void ShouldLoadFirstGridFromTheListOnStart()
        {
            //given
            var activeWindow = A.Fake<WindowRepresentation>();
            var windowDimensions = new Dimensions(new Point(0, 0), new Size(100, 100));
            A.CallTo(() => activeWindow.Dimensions).Returns(windowDimensions);

            var windowManager = A.Fake<WindowManager>();
            A.CallTo(() => windowManager.GetActiveWindow()).Returns(activeWindow);

            var dummyHotkeyConfiguration = A.Fake<GridHotkeyConfiguration>();
            A.CallTo(() => dummyHotkeyConfiguration.Left).Returns(A.Fake<GlobalHotkey>());
            A.CallTo(() => dummyHotkeyConfiguration.Right).Returns(A.Fake<GlobalHotkey>());
            A.CallTo(() => dummyHotkeyConfiguration.Up).Returns(A.Fake<GlobalHotkey>());
            A.CallTo(() => dummyHotkeyConfiguration.Down).Returns(A.Fake<GlobalHotkey>());

            var gridElementDimensions = new Dimensions(new Point(0, 0), new Size(1, 1));

            var windowsOnGridController = new WindowsOnGridController(dummyHotkeyConfiguration);
            var gridConfigs = new[]
            {
                new GridConfig
                {
                    Name = "asdf", GridElements = new [] { gridElementDimensions }
                }
            };
            var gridFactory = new GridFactory(windowManager);
            new GridSwitcher(gridConfigs, gridFactory, windowsOnGridController);

            //when
            dummyHotkeyConfiguration.Left.HotkeyPressed += Raise.WithEmpty();

            //then
            A.CallTo(() => activeWindow.SetDimensions(A<Dimensions>.That.Matches(newDimensions => newDimensions.Equals(gridElementDimensions)))).MustHaveHappened();
        }
    }
}
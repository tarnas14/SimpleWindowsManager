namespace Tests
{
    using System;
    using Common;
    using Common.Hotkeys;
    using Common.Windows;
    using FakeItEasy;
    using Halp;
    using NUnit.Framework;
    using SimpleWindowsManager.WindowGrid;
    using SimpleWindowsManager.WindowGrid.GridSystem;

    [TestFixture]
    class WindowsOnGridControllerSpecification
    {
        private GridHotkeyConfiguration _dummyHotkeyConfiguration;

        [SetUp]
        public void Setup()
        {
            _dummyHotkeyConfiguration = A.Fake<GridHotkeyConfiguration>();
            A.CallTo(() => _dummyHotkeyConfiguration.Left).Returns(A.Fake<GlobalHotkey>());
            A.CallTo(() => _dummyHotkeyConfiguration.Right).Returns(A.Fake<GlobalHotkey>());
            A.CallTo(() => _dummyHotkeyConfiguration.Up).Returns(A.Fake<GlobalHotkey>());
            A.CallTo(() => _dummyHotkeyConfiguration.Down).Returns(A.Fake<GlobalHotkey>());
        }

        [Test]
        public void ShouldMoveActiveWindowAroundLoadedGridWhenHotkeyPressed()
        {
            //given
            var activeWindow = A.Fake<WindowRepresentation>();
            var dimensions = new Dimensions(new Point(0 ,0), new Size(100, 100));
            A.CallTo(() => activeWindow.Dimensions).Returns(dimensions);

            var windowManager = A.Fake<WindowManager>();
            A.CallTo(() => windowManager.GetActiveWindow()).Returns(activeWindow);

            var grid = new Grid(windowManager);
            var gridElement = new SquareGridElement(new Dimensions(new Point(0,0), new Size(1, 1)));
            grid.AddElement(gridElement);

            var windowsOnGridController = new WindowsOnGridController(_dummyHotkeyConfiguration);
            windowsOnGridController.LoadGrid(grid);

            //when
            _dummyHotkeyConfiguration.Left.HotkeyPressed += Raise.WithEmpty();
            _dummyHotkeyConfiguration.Right.HotkeyPressed += Raise.WithEmpty();
            _dummyHotkeyConfiguration.Up.HotkeyPressed += Raise.WithEmpty();
            _dummyHotkeyConfiguration.Down.HotkeyPressed += Raise.WithEmpty();

            //then
            A.CallTo(() => activeWindow.SetDimensions(gridElement.Dimensions)).MustHaveHappened(Repeated.Exactly.Times(4));
        }

        [Test]
        public void ShouldNotNotifyOldGridAboutHotkeysWHenNewGridIsLoaded()
        {
            //given
            var activeWindow = A.Fake<WindowRepresentation>();

            var windowManager = A.Fake<WindowManager>();
            A.CallTo(() => windowManager.GetActiveWindow()).Returns(activeWindow);

            var firstLoadedGrid = new Grid(windowManager);
            var firstLoadedGridElement = new SquareGridElement(new Dimensions(new Point(0, 0), new Size(20, 20)));
            firstLoadedGrid.AddElement(firstLoadedGridElement);
            var secondLoadedGrid = new Grid(windowManager);
            var secondLoadedGridElement = new SquareGridElement(new Dimensions(new Point(20, 20), new Size(20, 20)));
            secondLoadedGrid.AddElement(secondLoadedGridElement);

            var windowsOnGridController = new WindowsOnGridController(_dummyHotkeyConfiguration);
            windowsOnGridController.LoadGrid(firstLoadedGrid);
            windowsOnGridController.LoadGrid(secondLoadedGrid);

            //when
            _dummyHotkeyConfiguration.Left.HotkeyPressed += Raise.WithEmpty();
            _dummyHotkeyConfiguration.Right.HotkeyPressed += Raise.WithEmpty();
            _dummyHotkeyConfiguration.Up.HotkeyPressed += Raise.WithEmpty();
            _dummyHotkeyConfiguration.Down.HotkeyPressed += Raise.WithEmpty();

            //then
            A.CallTo(() => activeWindow.SetDimensions(firstLoadedGridElement.Dimensions)).MustNotHaveHappened();
            A.CallTo(() => activeWindow.SetDimensions(secondLoadedGridElement.Dimensions)).MustHaveHappened(Repeated.Exactly.Times(4));
        }

        [Test]
        public void ShouldAddAndRemoveHandlersFromFakeEvent()
        {
            //given
            var globalHotkey = A.Fake<GlobalHotkey>();

            var counter = 0;

            EventHandler firstHandler = (sender, args) => counter++;
            EventHandler secondHandler = (sender, args) => counter++;

            globalHotkey.HotkeyPressed += firstHandler;
            globalHotkey.HotkeyPressed += secondHandler;
            globalHotkey.HotkeyPressed -= firstHandler;

            //when
            globalHotkey.HotkeyPressed += Raise.WithEmpty();

            //then
            Assert.That(counter, Is.EqualTo(1));
        }
    }
}

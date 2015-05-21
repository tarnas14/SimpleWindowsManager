namespace Tests
{
    using Common;
    using Common.Windows;
    using FakeItEasy;
    using Halp;
    using NUnit.Framework;
    using SimpleWindowsManager.WindowGrid.Configuration;
    using SimpleWindowsManager.WindowGrid.GridSystem;

    [TestFixture]
    class GridSpecification
    {
        [Test]
        [TestCase(GridDirections.Left)]
        [TestCase(GridDirections.Right)]
        [TestCase(GridDirections.Up)]
        [TestCase(GridDirections.Down)]
        [TestCase(GridDirections.Any)]
        public void ShouldInsertUngridedWindowIntoMainGridElementFirstRegardlessOfDirection(GridDirections direction)
        {
            //given
            var windowOutsideGrid = A.Fake<WindowRepresentation>();
            var grid = new Grid();
            var gridElement = new GridElement(new Dimensions(new Point(0, 0), new Size(100, 100)));
            grid.AddElement(gridElement);
            grid.SetAsMain(gridElement);

            //when
            grid.Move(windowOutsideGrid, direction);

            //then
            A.CallTo(
                () => windowOutsideGrid.SetDimensions(A<Dimensions>.That.Matches(dimensions => dimensions.Equals(gridElement.Dimensions)))).MustHaveHappened();
        }

        [Test]
        [TestCase(GridDirections.Left, 0, 0, 0, 0)]
        [TestCase(GridDirections.Up, 1, 1, 1, 1)]
        [TestCase(GridDirections.Right, 2, 2, 2, 2)]
        [TestCase(GridDirections.Down, 3, 3, 3, 3)]
        public void ShouldCallRespectiveGridElementsWhenMovingWindowAlreadyOnGrid(GridDirections moveDirection, int expectedX, int expectedY, int expectedWidth, int expectedHeight)
        {
            //given
            var windowOutsideGrid = new DummyWindowRepresentation();
            var grid = new Grid();
            var leftGridElement = new GridElement(new Dimensions(new Point(0, 0), new Size(0, 0)));
            var topGridElement = new GridElement(new Dimensions(new Point(1, 1), new Size(1, 1)));
            var rightGridElement = new GridElement(new Dimensions(new Point(2, 2), new Size(2, 2)));
            var bottomGridElement = new GridElement(new Dimensions(new Point(3, 3), new Size(3, 3)));

            var mainGridElement = new GridElement(new Dimensions(new Point(666, 666), new Size(666, 666)));
            mainGridElement.SetNeighbour(leftGridElement, GridDirections.Left);
            mainGridElement.SetNeighbour(topGridElement, GridDirections.Up);
            mainGridElement.SetNeighbour(rightGridElement, GridDirections.Right);
            mainGridElement.SetNeighbour(bottomGridElement, GridDirections.Down);

            grid.AddElement(mainGridElement);
            grid.SetAsMain(mainGridElement);
            grid.Move(windowOutsideGrid, GridDirections.Any);

            var expectedDimensions = new Dimensions(new Point(expectedX, expectedY),
                new Size(expectedWidth, expectedHeight));

            //when
            grid.Move(windowOutsideGrid, moveDirection);

            //then
            Assert.That(windowOutsideGrid.Dimensions, Is.EqualTo(expectedDimensions));
        }
    }

    [TestFixture]
    class GridElementSpecification
    {
        [Test]
        public void ShouldRecognizeIfWindowIsInGridOrNot()
        {
            //given
            var gridElementDimensions = new Dimensions(new Point(1, 2), new Size(3, 4));
            var dimensionsOutsideGrid = new Dimensions(new Point(5, 6), new Size(7, 8));

            var gridElement = new GridElement(gridElementDimensions);

            var windowOutsideGrid = A.Fake<WindowRepresentation>();
            A.CallTo(() => windowOutsideGrid.Dimensions).Returns(dimensionsOutsideGrid);

            var windowInsideGrid = A.Fake<WindowRepresentation>();
            A.CallTo(() => windowInsideGrid.Dimensions).Returns(gridElementDimensions);

            //then
            Assert.That(gridElement.HasWindow(windowInsideGrid));
            Assert.That(!gridElement.HasWindow(windowOutsideGrid));
        }

        [Test]
        public void ShouldSetWindowDimensions()
        {
            //given
            var window = A.Fake<WindowRepresentation>();
            var gridElement = new GridElement(new Dimensions(new Point(1, 2), new Size(3, 4)));

            //when
            gridElement.SetWindow(window);

            //then
            A.CallTo(
                () =>
                    window.SetDimensions(
                        A<Dimensions>.That.Matches(dimensions => dimensions.Equals(gridElement.Dimensions))));
        }
    }

    [TestFixture]
    class GridFactorySpecification
    {
        [Test]
        public void ShouldCorrectlyBuildGrid()
        {
            //given
            var topLeft = new Dimensions(new Point(0, 0), new Size(1, 1));
            var topRight = new Dimensions(new Point(1, 0), new Size(1, 1));
            var bottomRight = new Dimensions(new Point(1, 1), new Size(1, 1));
            var bottomLeft = new Dimensions(new Point(0, 1), new Size(1, 1));

            var config = new GridConfig
            {
                GridElements = new[]
                {
                    topLeft, topRight, bottomRight, bottomLeft
                },
                NeighbourMap = new[]
                {
                    new [] { 3, 1, 3, 1 },
                    new [] { 2, 0, 2, 0 },
                    new [] { 1, 3, 1, 3 },
                    new [] { 0, 2, 0, 2 }
                },
                MainElement = 0
            };
            var grid = GridFactory.FromConfig(config);
            var dummyWindow = new DummyWindowRepresentation();

            //when
            grid.Move(dummyWindow, GridDirections.Left);
            Assert.That(dummyWindow.Dimensions, Is.EqualTo(topLeft));

            //when
            grid.Move(dummyWindow, GridDirections.Right);
            //then
            Assert.That(dummyWindow.Dimensions, Is.EqualTo(topRight));

            //when
            grid.Move(dummyWindow, GridDirections.Down);
            //then
            Assert.That(dummyWindow.Dimensions, Is.EqualTo(bottomRight));

            //when
            grid.Move(dummyWindow, GridDirections.Left);
            //then
            Assert.That(dummyWindow.Dimensions, Is.EqualTo(bottomLeft));
        }
    }
}

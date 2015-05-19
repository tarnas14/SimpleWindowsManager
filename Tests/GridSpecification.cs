namespace Tests
{
    using Common;
    using Common.GridSystem;
    using Common.Windows;
    using FakeItEasy;
    using NUnit.Framework;

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
            var windowOutsideGrid = A.Fake<WindowRepresentation>();
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
            A.CallTo(
                () => windowOutsideGrid.SetDimensions(A<Dimensions>.That.Matches(dimensions => dimensions.Equals(expectedDimensions)))).MustHaveHappened();
        }
    }

    [TestFixture]
    class GridElementSpecification
    {
        [Test]
        public void ShouldSetWindowDimensionsAndRememberIt()
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
            Assert.That(gridElement.HasWindow(window));
        }

        [Test]
        public void ShouldMoveWindowInSpecifiedDirection()
        {
            //given
            var window = A.Fake<WindowRepresentation>();
            var gridElement = new GridElement(new Dimensions(new Point(1, 2), new Size(3, 4)));
            var gridElementToTheRight = new GridElement(new Dimensions(new Point(5, 6), new Size(7, 8)));
            gridElement.SetNeighbour(gridElementToTheRight, GridDirections.Right);

            gridElement.SetWindow(window);

            //when
            gridElement.Move(window, GridDirections.Right);

            //then
            Assert.That(!gridElement.HasWindow(window));
            Assert.That(gridElementToTheRight.HasWindow(window));
        }

        [Test]
        public void ShouldNotRecognizeWindowThatWasNotSetIntoGrid()
        {
            //given
            var window = A.Fake<WindowRepresentation>();
            var gridElement = new GridElement(new Dimensions(new Point(1, 2), new Size(3, 4)));

            //when
            //nothing, really

            //then
            Assert.That(!gridElement.HasWindow(window));
        }
    }
}

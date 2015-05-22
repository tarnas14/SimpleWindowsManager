namespace Tests
{
    using Common;
    using Common.Windows;
    using FakeItEasy;
    using Halp;
    using NUnit.Framework;
    using SimpleWindowsManager.WindowGrid.GridSystem;

    [TestFixture]
    class GridSpecification
    {
        private GridElement _leftTop;
        private GridElement _centerTop;
        private GridElement _leftCenter;
        private GridElement _centerCenter;
        private Size _quarterSize;
        private Grid _quarterGrid;
        private Dimensions _dimensionsOutsideGrid;

        [SetUp]
        public void Setup()
        {
            _quarterSize = new Size(960, 540);
            _leftTop = new GridElement(new Dimensions(new Point(0, 0), _quarterSize));
            _centerTop = new GridElement(new Dimensions(new Point(960, 0), _quarterSize));
            _leftCenter = new GridElement(new Dimensions(new Point(0, 540), _quarterSize));
            _centerCenter = new GridElement(new Dimensions(new Point(960, 540), _quarterSize));

            _leftTop.SetNeighbour(_centerTop, GridDirections.Left);
            _leftTop.SetNeighbour(_centerTop, GridDirections.Right);
            _leftTop.SetNeighbour(_leftCenter, GridDirections.Up);
            _leftTop.SetNeighbour(_leftCenter, GridDirections.Down);
            _centerTop.SetNeighbour(_leftTop, GridDirections.Left);
            _centerTop.SetNeighbour(_leftTop, GridDirections.Right);
            _centerTop.SetNeighbour(_centerCenter, GridDirections.Down);
            _centerTop.SetNeighbour(_centerCenter, GridDirections.Up);
            _leftCenter.SetNeighbour(_centerCenter, GridDirections.Right);
            _leftCenter.SetNeighbour(_centerCenter, GridDirections.Left);
            _leftCenter.SetNeighbour(_leftTop, GridDirections.Up);
            _leftCenter.SetNeighbour(_leftTop, GridDirections.Down);
            _centerCenter.SetNeighbour(_leftCenter, GridDirections.Right);
            _centerCenter.SetNeighbour(_leftCenter, GridDirections.Left);
            _centerCenter.SetNeighbour(_centerTop, GridDirections.Down);
            _centerCenter.SetNeighbour(_centerTop, GridDirections.Up);

            _quarterGrid = new Grid();
            _quarterGrid.AddElement(_leftTop);
            _quarterGrid.AddElement(_leftCenter);
            _quarterGrid.AddElement(_centerCenter);
            _quarterGrid.AddElement(_centerTop);

            _dimensionsOutsideGrid = new Dimensions(new Point(480, 270), _quarterSize);
        }

        [Test]
        [TestCase(GridDirections.Left, 960, 0, TestName = "leftTop -left-> centerTop")]
        [TestCase(GridDirections.Up, 0, 540, TestName = "leftTop -up-> leftCenter")]
        [TestCase(GridDirections.Right, 960, 0, TestName = "leftTop -right-> centerTop")]
        [TestCase(GridDirections.Down, 0, 540, TestName = "leftTop -down-> leftCenter")]
        public void ShouldCallRespectiveGridElementsWhenMovingWindowAlreadyOnGrid(GridDirections moveDirection, int expectedX, int expectedY)
        {
            //given
            var windowInLeftTopQuarter = new DummyWindowRepresentation()
            {
                Dimensions = _leftTop.Dimensions
            };

            var expectedDimensions = new Dimensions(new Point(expectedX, expectedY),
                _quarterSize);

            //when
            _quarterGrid.Move(windowInLeftTopQuarter, moveDirection);

            //then
            Assert.That(windowInLeftTopQuarter.Dimensions, Is.EqualTo(expectedDimensions));
        }

        [Test]
        [TestCase(GridDirections.Left, 0)]
        [TestCase(GridDirections.Right, 960)]
        public void ShouldMoveToClosestGridOriginWhenMovingHorizontally(GridDirections horizontalDirection, int originXCoordinate)
        {
            //given
            var centerHorizontallyAlignedWindow = new DummyWindowRepresentation
            {
                Dimensions = new Dimensions(new Point(900, 540), new Size(0,0))
            };

            const int expectedYOriginCoordinate = 540;
            var expectedDimensions = new Dimensions(new Point(originXCoordinate, expectedYOriginCoordinate),_quarterSize);

            //when
            _quarterGrid.Move(centerHorizontallyAlignedWindow, horizontalDirection);

            //then
            Assert.That(centerHorizontallyAlignedWindow.Dimensions, Is.EqualTo(expectedDimensions));
        }

        [Test]
        [TestCase(GridDirections.Up, 0)]
        [TestCase(GridDirections.Down, 540)]
        public void ShouldMoveToClosestGridOriginWhenMovingVertically(GridDirections verticalDirection, int originYCoordinate)
        {
            //given
            var centerVerticallyAlignedWindow = new DummyWindowRepresentation
            {
                Dimensions = new Dimensions(new Point(960, 500), new Size(0, 0))
            };

            const int expectedXOriginCoordinate = 960;
            var expectedDimensions = new Dimensions(new Point(expectedXOriginCoordinate, originYCoordinate), _quarterSize);

            //when
            _quarterGrid.Move(centerVerticallyAlignedWindow, verticalDirection);

            //then
            Assert.That(centerVerticallyAlignedWindow.Dimensions, Is.EqualTo(expectedDimensions));
        }

        [Test]
        [TestCase(GridDirections.Right)]
        [TestCase(GridDirections.Left)]
        [TestCase(GridDirections.Up)]
        [TestCase(GridDirections.Down)]
        public void ShouldMoveWindowToTheFirstGridElementIfItIsNotAlignedWithAnyGridElement(GridDirections direction)
        {
            //given
            var windowNotAlignedWithAnyGridElement = new DummyWindowRepresentation
            {
                Dimensions = new Dimensions(new Point(800, 500), new Size(0, 0))
            };

            var expectedDimensions = _leftTop.Dimensions;

            //when
            _quarterGrid.Move(windowNotAlignedWithAnyGridElement, direction);

            //then
            Assert.That(windowNotAlignedWithAnyGridElement.Dimensions, Is.EqualTo(expectedDimensions));
        }
    }
}

﻿namespace Tests
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
        private SquareGridElement _leftTop;
        private SquareGridElement _centerTop;
        private SquareGridElement _leftCenter;
        private SquareGridElement _centerCenter;
        private Size _quarterSize;
        private Grid _quarterGrid;
        private readonly Dimensions _theMiddle = new Dimensions(new Point(480, 270), new Size(480, 270));
        private WindowManager _windowManager;

        //X****X*****
        //*    *    *
        //*    *    *
        //X****X*****
        //*    *    *
        //*    *    *
        //***********
        //(0,0), (960,0), (0,540), (960,540)
        [SetUp]
        public void Setup()
        {
            _windowManager = A.Fake<WindowManager>();

            _quarterSize = new Size(960, 540);
            _leftTop = new SquareGridElement(new Dimensions(new Point(0, 0), _quarterSize));
            _centerTop = new SquareGridElement(new Dimensions(new Point(960, 0), _quarterSize));
            _leftCenter = new SquareGridElement(new Dimensions(new Point(0, 540), _quarterSize));
            _centerCenter = new SquareGridElement(new Dimensions(new Point(960, 540), _quarterSize));

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

            _quarterGrid = new Grid(_windowManager);
            _quarterGrid.AddElement(_leftTop);
            _quarterGrid.AddElement(_leftCenter);
            _quarterGrid.AddElement(_centerCenter);
            _quarterGrid.AddElement(_centerTop);
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
            A.CallTo(() => _windowManager.GetActiveWindow()).Returns(windowInLeftTopQuarter);
    
            var expectedDimensions = new Dimensions(new Point(expectedX, expectedY),
                _quarterSize);

            //when
            _quarterGrid.MoveActiveWindow(moveDirection);

            //then
            Assert.That(windowInLeftTopQuarter.Dimensions, Is.EqualTo(expectedDimensions));
        }

        [Test]
        [TestCase(GridDirections.Left, 900, 540, 0, 540, TestName = "900,540-left-0,540")]
        [TestCase(GridDirections.Right, 900, 540, 960, 540, TestName = "900,540-right-960,540")]
        [TestCase(GridDirections.Up, 960, 500, 960, 0, TestName = "960,500-up-960,0")]
        [TestCase(GridDirections.Down, 960, 500, 960, 540, TestName = "960,500-down-960,540")]
        [TestCase(GridDirections.Left, 479, 269, 0, 0, TestName = "479,269-left-0,0")]
        [TestCase(GridDirections.Left, 479, 271, 0, 540, TestName = "479,271-left-0,540")]
        [TestCase(GridDirections.Right, 481, 269, 960, 0, TestName = "481,269-left-960,0")]
        [TestCase(GridDirections.Right, 481, 271, 960, 540, TestName = "481,271-right-960,540")]
        [TestCase(GridDirections.Up, 479, 269, 0, 0, TestName = "479,269-up-0,0")]
        [TestCase(GridDirections.Down, 479, 271, 0, 540, TestName = "479,271-down-0,540")]
        [TestCase(GridDirections.Up, 481, 269, 960, 0, TestName = "481,269-up-960,0")]
        [TestCase(GridDirections.Down, 481, 271, 960, 540, TestName = "481,271-down-960,540")]
        public void ShouldMoveWindowToTheGridElementWithOriginClosestToTheWindowInTheDirectionWeWantToMoveIt(GridDirections direction, int windowX, int windowY, int expectedX, int expectedY)
        {
            //given
            var window = new DummyWindowRepresentation
            {
                Dimensions = new Dimensions(new Point(windowX, windowY), _quarterSize)
            };
            A.CallTo(() => _windowManager.GetActiveWindow()).Returns(window);
            var expectedDimensions = new Dimensions(new Point(expectedX, expectedY), _quarterSize);

            //when
            _quarterGrid.MoveActiveWindow(direction);

            //then
            Assert.That(window.Dimensions, Is.EqualTo(expectedDimensions));
        }

        [Test]
        [TestCase(GridDirections.Left, 0, 0)]
        [TestCase(GridDirections.Right, 960, 0)]
        public void ShouldChooseTopWindowIfMultipleGridElementsFoundInTheSameDistanceWhenMovingHorizontally(GridDirections direction, int expectedX, int expectedY)
        {
            //given
            var windowExactlyInTheMiddle = new DummyWindowRepresentation
            {
                Dimensions = _theMiddle
            };
            A.CallTo(() => _windowManager.GetActiveWindow()).Returns(windowExactlyInTheMiddle);
            var expectedDimensions = new Dimensions(new Point(expectedX, expectedY), _quarterSize);

            //when
            _quarterGrid.MoveActiveWindow(direction);

            //then
            Assert.That(windowExactlyInTheMiddle.Dimensions, Is.EqualTo(expectedDimensions));
        }


        [Test]
        [TestCase(GridDirections.Up, 0, 0)]
        [TestCase(GridDirections.Down, 0, 540)]
        public void ShouldChooseLeftWindowIfMultipleGridElementsFoundInTheSameDistanceWhenMovingVertically(GridDirections direction, int expectedX, int expectedY)
        {
            //given
            var windowExactlyInTheMiddle = new DummyWindowRepresentation
            {
                Dimensions = _theMiddle
            };
            A.CallTo(() => _windowManager.GetActiveWindow()).Returns(windowExactlyInTheMiddle);
            var expectedDimensions = new Dimensions(new Point(expectedX, expectedY), _quarterSize);

            //when
            _quarterGrid.MoveActiveWindow(direction);

            //then
            Assert.That(windowExactlyInTheMiddle.Dimensions, Is.EqualTo(expectedDimensions));
        }
    }
}

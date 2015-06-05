namespace Tests
{
    using Common;
    using Common.Windows;
    using FakeItEasy;
    using Halp;
    using NUnit.Framework;
    using SimpleWindowsManager.WindowGrid.Configuration;

    [TestFixture]
    class GridFactorySpecification
    {
        private WindowManager _windowManager;

        private GridFactory _gridFactory;

        [SetUp]
        public void Setup()
        {
            _windowManager = A.Fake<WindowManager>();
            _gridFactory = new GridFactory(_windowManager);
        }

        //X****X*****
        [Test]
        public void ShouldInjectWindowManagerIntoGrid()
        {
            //given

            var gridConfig = new GridConfig
            {
                GridElements = new[]
                {
                    new Dimensions(new Point(0, 0), new Size(300, 300)), 
                }
            };
            var grid = _gridFactory.FromConfig(gridConfig);

            //when
            grid.MoveActiveWindow(GridDirections.Left);
            //then

            A.CallTo(() => _windowManager.GetActiveWindow()).MustHaveHappened();
        }

        //*    *    *
        //*    *    *
        //X****X*****
        //*    *    *
        //*    *    *
        //***********
        //(0,0), (960,0), (0,540), (960,540)
        //size: 960x540
        [Test]
        public void ShouldCorrectlyBuildGridWithFullNeighbourMap()
        {
            //given
            var size = new Size(960, 540);
            var topLeft = new Dimensions(new Point(0, 0), size);
            var topRight = new Dimensions(new Point(960, 0), size);
            var bottomLeft = new Dimensions(new Point(0, 540), size);
            var bottomRight = new Dimensions(new Point(960, 540), size);

            var config = new GridConfig
            {
                GridElements = new[]
                {
                    topLeft, topRight, bottomLeft, bottomRight
                },
                NeighbourMap = new[]
                {
                    new NeighboursMap
                    {
                        Id = 0,
                        Neighbours = new [] { 3, 1, 3, 1 }
                    },
                    new NeighboursMap
                    {
                        Id = 1,
                        Neighbours = new [] { 2, 0, 2, 0 }
                    },
                    new NeighboursMap
                    {
                        Id = 2,
                        Neighbours = new [] { 1, 3, 1, 3 }
                    },
                    new NeighboursMap
                    {
                        Id = 3,
                        Neighbours = new [] { 0, 2, 0, 2 }
                    }
                }
            };
            var grid = _gridFactory.FromConfig(config);
            var dummyWindow = new DummyWindowRepresentation
            {
                Dimensions = new Dimensions(new Point(0, 0), new Size(55, 55))
            };
            A.CallTo(() => _windowManager.GetActiveWindow()).Returns(dummyWindow);

            //when
            grid.MoveActiveWindow(GridDirections.Left);
            Assert.That(dummyWindow.Dimensions, Is.EqualTo(topLeft));

            //when
            grid.MoveActiveWindow(GridDirections.Right);
            //then
            Assert.That(dummyWindow.Dimensions, Is.EqualTo(topRight));

            //when
            grid.MoveActiveWindow(GridDirections.Down);
            //then
            Assert.That(dummyWindow.Dimensions, Is.EqualTo(bottomLeft));

            //when
            grid.MoveActiveWindow(GridDirections.Left);
            //then
            Assert.That(dummyWindow.Dimensions, Is.EqualTo(bottomRight));
        }

        //X*****
        //*    *
        //*    *
        //******
        //(0,0), (960,0), (0,540), (960,540)
        //size: 960x540
        //
        [Test]
        public void ShouldNotMoveWindowIfItHasNoNeighbour()
        {
            //given
            var size = new Size(960, 540);
            var gridElement = new Dimensions(new Point(0, 0), size);

            var config = new GridConfig
            {
                GridElements = new[]
                {
                    gridElement
                }
            };

            var grid = _gridFactory.FromConfig(config);
            var dummyWindow = new DummyWindowRepresentation
            {
                Dimensions = new Dimensions(new Point(1, 2), new Size(55, 55))
            };
            A.CallTo(() => _windowManager.GetActiveWindow()).Returns(dummyWindow);

            //when
            grid.MoveActiveWindow(GridDirections.Left);
            //then
            Assert.That(dummyWindow.Dimensions, Is.EqualTo(gridElement));

            //when
            grid.MoveActiveWindow(GridDirections.Right);
            //then
            Assert.That(dummyWindow.Dimensions, Is.EqualTo(gridElement));

            //when
            grid.MoveActiveWindow(GridDirections.Down);
            //then
            Assert.That(dummyWindow.Dimensions, Is.EqualTo(gridElement));

            //when
            grid.MoveActiveWindow(GridDirections.Left);
            //then
            Assert.That(dummyWindow.Dimensions, Is.EqualTo(gridElement));

            //when
            grid.MoveActiveWindow(GridDirections.Up);
            //then
            Assert.That(dummyWindow.Dimensions, Is.EqualTo(gridElement));
        }

        //X****X*****

        //*    *    *

        //*    *    *

        //X****X*****

        //*    *    *

        //*    *    *

        //***********

        //(0,0), (960,0), (0,540), (960,540)

        //size: 960x540

        [Test]
        public void ShouldCorrectlyBuildGridWithPartialNeighbourMap()
        {
            //given
            var size = new Size(960, 540);
            var topLeft = new Dimensions(new Point(0, 0), size);
            var topRight = new Dimensions(new Point(960, 0), size);
            var bottomLeft = new Dimensions(new Point(0, 540), size);
            var bottomRight = new Dimensions(new Point(960, 540), size);

            var config = new GridConfig
            {
                GridElements = new[]
                {
                    topLeft, topRight, bottomLeft, bottomRight
                },
                NeighbourMap = new[]
                {
                    new NeighboursMap
                    {
                        Id = 0,
                        Neighbours = new [] { 2, 1, 2, 1 }
                    }
                }
            };
            var dummyWindow = new DummyWindowRepresentation
            {
                Dimensions = new Dimensions(new Point(0, 0), new Size(55, 55))
            };
            A.CallTo(() => _windowManager.GetActiveWindow()).Returns(dummyWindow);

            var gridFactory = _gridFactory;
            var grid = gridFactory.FromConfig(config);

            //when
            grid.MoveActiveWindow(GridDirections.Left);
            //then
            Assert.That(dummyWindow.Dimensions, Is.EqualTo(topLeft));

            //when
            grid.MoveActiveWindow(GridDirections.Right);
            //then
            Assert.That(dummyWindow.Dimensions, Is.EqualTo(topRight));
        }
    }
}
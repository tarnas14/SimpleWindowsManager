namespace Tests
{
    using Common;
    using Halp;
    using NUnit.Framework;
    using SimpleWindowsManager.WindowGrid.Configuration;

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
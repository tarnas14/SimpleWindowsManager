namespace Tests
{
    using Common;
    using Halp;
    using NUnit.Framework;
    using SimpleWindowsManager.WindowGrid.Configuration;

    [TestFixture]
    class GridFactorySpecification
    {
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
        public void ShouldCorrectlyBuildGrid()
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
                    new [] { 3, 1, 3, 1 },
                    new [] { 2, 0, 2, 0 },
                    new [] { 1, 3, 1, 3 },
                    new [] { 0, 2, 0, 2 }
                }
            };
            var grid = GridFactory.FromConfig(config);
            var dummyWindow = new DummyWindowRepresentation
            {
                Dimensions = new Dimensions(new Point(0, 0), new Size(55, 55))
            };

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
            Assert.That(dummyWindow.Dimensions, Is.EqualTo(bottomLeft));

            //when
            grid.Move(dummyWindow, GridDirections.Left);
            //then
            Assert.That(dummyWindow.Dimensions, Is.EqualTo(bottomRight));
        }
    }
}
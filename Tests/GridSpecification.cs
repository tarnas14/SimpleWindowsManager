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
    }
}

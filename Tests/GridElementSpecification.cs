namespace Tests
{
    using Common;
    using Common.Windows;
    using FakeItEasy;
    using NUnit.Framework;
    using SimpleWindowsManager.WindowGrid.GridSystem;

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
}
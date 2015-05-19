namespace Tests.Halp
{
    using Common;
    using Common.Windows;

    class DummyWindowRepresentation : WindowRepresentation
    {
        public void SetDimensions(Dimensions dimensions)
        {
            Dimensions = dimensions;
        }

        public Dimensions Dimensions { get; set; }
    }
}
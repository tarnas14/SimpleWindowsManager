namespace Common
{
    public class Dimensions
    {
        public Point Origin { get; private set; }
        public Size Size { get; private set; }

        public Dimensions(Point origin, Size size)
        {
            Origin = origin;
            Size = size;
        }

        public override bool Equals(object obj)
        {
            var otherDimensions = obj as Dimensions;

            if (otherDimensions == null)
            {
                return false;
            }

            return otherDimensions.Origin.Equals(Origin) && otherDimensions.Size.Equals(Size);
        }
    }
}
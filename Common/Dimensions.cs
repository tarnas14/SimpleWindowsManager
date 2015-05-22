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

        public override string ToString()
        {
            return string.Format("[({0},{1}), ({2}x{3})]", Origin.X, Origin.Y, Size.Width, Size.Height);
        }
    }
}
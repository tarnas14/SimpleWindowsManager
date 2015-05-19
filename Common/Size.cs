namespace Common
{
    public class Size
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override bool Equals(object obj)
        {
            var otherSize = obj as Size;

            if (otherSize == null)
            {
                return false;
            }

            return otherSize.Width == Width && otherSize.Height == Height;
        }
    }
}
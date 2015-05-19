namespace Common.GridSystem
{
    public class GridElement
    {
        public GridElement(Dimensions dimensions)
        {
            Dimensions = dimensions;
        }

        public Dimensions Dimensions { get; private set; }
    }
}
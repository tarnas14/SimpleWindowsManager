namespace Common.Windows
{
    public interface WindowRepresentation
    {
        void SetDimensions(Dimensions dimensions);
        Dimensions Dimensions { get; }
    }
}
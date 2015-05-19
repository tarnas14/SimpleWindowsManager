namespace Common.Windows
{
    public interface WindowRepresentation
    {
        void SetDimensions(Dimensions matches);
        int Id { get; }
    }
}
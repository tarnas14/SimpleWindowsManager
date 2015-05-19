namespace Common.Windows
{
    public interface ICanBeSearchedFor
    {
        bool Matches(string searchExpression);
        int Id { get; }
    }
}
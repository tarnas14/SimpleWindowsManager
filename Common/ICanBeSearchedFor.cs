namespace Common
{
    public interface ICanBeSearchedFor
    {
        bool Matches(string searchExpression);
        int Id { get; }
    }
}
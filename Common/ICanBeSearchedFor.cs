namespace Common
{
    public interface ICanBeSearchedFor
    {
        bool Matches(string searchExpression);
    }
}
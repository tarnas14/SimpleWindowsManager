namespace Common.Configuration
{
    public interface Configuration<out T> where T : Configuration<T>
    {
        T Default { get; }
    }
}
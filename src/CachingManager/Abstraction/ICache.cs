namespace CachingManager.Abstraction
{
    public interface ICache
    {
        string Name { get; }

        long SizeLimit { get; }

        void Clear();
    }
}

namespace CachingManager.Abstraction
{
    public interface ICache<TValue, TKey> : ICache where TValue : IMesurable
    {
        TValue this[TKey key] { get; set; }

        void Set(TKey key, TValue value);

        TValue Get(TKey key, TValue defaultValue = default(TValue));

        bool Exists(TKey key);

        void Remove(TKey key);
    }
}

using System;

namespace CachingManager.Abstraction
{
    public interface ICacheEntry<TValue, TKey> where TValue : IMesurable
    {
        TKey Key { get; }

        TValue Value { get; }

        DateTime CreatedUtc { get; }

        DateTime LastAccessedUtc { get; }

        int HitCounter { get; }
    }
}

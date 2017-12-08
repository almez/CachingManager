using System;

namespace CachingManager.Abstraction
{
    public interface ICache : IDisposable
    {
        string Name { get; }

        long SizeLimit { get; }

        void Clear();
    }
}

using CachingManager.Abstraction;
using Microsoft.Extensions.Caching.Memory;

namespace CachingManager
{
    public class Cache<TValue, TKey> : ICache<TValue, TKey> where TValue : IMesurable
    {
        #region Public Properties

        public TValue this[TKey key]
        {
            get => this.Get(key);
            set => this.Set(key, value);
        }

        public string Name { get; }

        public long SizeLimit { get; }

        private MemoryCache _cache;

        #endregion

        #region c'tor

        internal Cache(string name, long sizeLimit)
        {
            this.Name = name;
            this.SizeLimit = sizeLimit;

            this.InitializeCache();
        }

        #endregion

        #region Public Methods

        public void Clear()
        {
            this._cache.Dispose();
            this.InitializeCache();
        }

        public bool Exists(TKey key)
        {
            return this.Get(key) != null;
        }

        public TValue Get(TKey key, TValue defaultValue = default(TValue))
        {
            var cacheEntry = _cache.Get<CacheEntry<TValue, TKey>>(key);

            return cacheEntry != null ? cacheEntry.Value : defaultValue;
        }

        public void Remove(TKey key)
        {
            _cache.Remove(key);
        }

        public void Set(TKey key, TValue value)
        {
            if (value == null)
            {
                _cache.Remove(key);
            }
            else
            {
                var cacheEntry = new CacheEntry<TValue, TKey>(key, value);

                _cache.Set(key, cacheEntry, new MemoryCacheEntryOptions()
                {
                    Size = value.GetSizeInBytes()
                });
            }
        }

        #endregion

        #region Private Methods

        private void InitializeCache()
        {
            this._cache = new MemoryCache(new MemoryCacheOptions()
            {
                SizeLimit = this.SizeLimit,
                CompactionPercentage = 0.1,
            });
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            _cache?.Dispose();
        }

        #endregion
    }
}

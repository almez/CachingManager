using System;
using System.Collections.Generic;
using System.Text;
using CachingManager.Abstraction;

namespace CachingManager
{
    public class CacheEntry<TValue, TKey> : ICacheEntry<TValue, TKey> where TValue : IMesurable
    {
        #region Public Properties

        public TKey Key { get; }
        public TValue Value { get; }
        public DateTime CreatedUtc { get; }
        public DateTime LastAccessedUtc { get; }
        public int HitCounter { get; }

        #endregion

        #region Cont'r

        public CacheEntry(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;

            this.CreatedUtc =  DateTime.UtcNow;
            this.LastAccessedUtc = this.CreatedUtc;
            this.HitCounter = 0;
        }

        #endregion
    }
}

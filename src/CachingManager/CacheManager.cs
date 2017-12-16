using System.Collections.Generic;
using System.Linq;
using CachingManager.Abstraction;

namespace CachingManager
{
    public sealed partial class CacheManager
    {
        #region Fields

        private static readonly List<ICache> _caches = new List<ICache>();

        private static readonly object _lock = new object();

        #endregion

        #region Singleton

        private static CacheManager _instance;

        public static CacheManager Instance
        {
            get
            {
                lock (_lock)
                {
                    return (_instance = _instance ?? new CacheManager());
                }
            }
        }

        #endregion

        #region C'tor

        private CacheManager() { }

        #endregion

        #region Public Methods


        public List<ICache> ListCaches()
        {
            return _caches;
        }

        public ICache FindCacheByName(string name)
        {
            return _caches.SingleOrDefault(x => x.Name == name);
        }

        public bool CacheExists(string name)
        {
            return _caches.Any(x => x.Name == name);
        }

        public void ClearAll()
        {
            _caches.ForEach(cache => cache.Clear());
        }

        #endregion


        #region Internal Methods

        internal void RegisterCache(ICache cache)
        {
            _caches.Add(cache);
        }

        #endregion
    }
}

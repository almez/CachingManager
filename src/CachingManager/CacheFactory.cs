using System.Data;
using CachingManager.Abstraction;

namespace CachingManager
{
    public class CacheFactory
    {
        public static Cache<TValue, TKey> CreateCache<TValue, TKey>(string name, long sizeLimit) where TValue : IMesurable
        {
            if (CacheManager.Instance.CacheExists(name))
            {
                throw new DuplicateNameException($"A cache with the same name [{name}] has been already registered in tha cache manager");
            }

            var cache = new Cache<TValue, TKey>(name, sizeLimit);

            CacheManager.Instance.RegisterCache(cache);

            return cache;
        }
    }
}

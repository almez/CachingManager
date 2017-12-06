# CachingManager

It's a wrapper library for [Microsoft.Extensions.Caching.Memory.MemoryCache] targeting .net core 2.0, allow you to create multiple instances from MEmoryCache Type and keep them all under your control in a centralized-place which is CacheManager.


## Build Server

 [![Build status](https://ci.appveyor.com/api/projects/status/a3uxxqdk0e6incv1?svg=true)](https://ci.appveyor.com/project/almez/cachingmanager-kw9tl)

 ## Kick-start

 * ##### How to create new cache instance
 ```
 using CachingManager;
 
 var cache = CacheFactory.CreateCache<User, int>("Users cache", 10 * 1024);
 ```
 
* ##### How to cache and retreive an object
 ```
 var user = new User() {Id = 10, Name = "Alden" };
 
 cache[user.Id] = user;
 
 var cachedCopy = cache[user.Id];
  ```
  
* ##### How to get list of existing caches.
 ```
List<ICache> caches = CacheManager.Instance.ListCaches();

foreach (var cache in caches)
{
    // clear cache
    cache.Clear();

    //cache properties
    string name = cache.Name;
    long sizeLimit = cache.SizeLimit;
}
  ```
  
* ##### How to clear all caches
 ```
 CacheManager.Instance.ClearAll();
 ```

## Contribute to this repository
Feel free to contribute to this repository, you can reach me out by the email eng.ngmalden@gmail.com


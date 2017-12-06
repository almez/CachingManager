using System;
using System.Data;
using CachingManager.Abstraction;
using Xunit;

namespace CachingManager.Tests
{
    public class CacheFactoryTest
    {
        #region Fields

        private readonly int _sizeLimit = 512 * 1024;

        #endregion

        [Fact(DisplayName = "Factory: registers new caches once created in cacheManager")]
        public void CreateCache_WithValidParameters_RegistersCahceInCacheManager()
        {
            //Arrange
            var cacheName = Guid.NewGuid().ToString();

            //Act
            ICache<User, int> cache = CacheFactory.CreateCache<User, int>(cacheName, _sizeLimit);
            var result = CacheManager.Instance.CacheExists(cacheName);

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Factory: Throws DuplicatedNameEXception for duplicated name cache.")]
        public void CreateCache_WithDuplicatedName_ThrowsDuplicatedNameException()
        {
            //Arrange
            var cacheName = Guid.NewGuid().ToString();
            ICache<User, int> cache1 = CacheFactory.CreateCache<User, int>(cacheName, _sizeLimit);

            //Act & Assert
            Exception ex = Assert.Throws<DuplicateNameException>(() =>
            {
                ICache<User, int> cache2 = CacheFactory.CreateCache<User, int>(cacheName, _sizeLimit);
            });
        }
    }
}

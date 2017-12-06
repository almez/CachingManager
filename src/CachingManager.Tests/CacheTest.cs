using System;
using System.Linq;
using CachingManager.Abstraction;
using Xunit;

namespace CachingManager.Tests
{
    public class CacheTest
    {
        #region Fields

        private ICache<User, int> _cache;

        private readonly int _sizeLimit = 512 * 1024;

        #endregion

        #region Setup

        private void Initialize()
        {
            _cache = CacheFactory.CreateCache<User, int>(Guid.NewGuid().ToString(), _sizeLimit);
        }

        #endregion

        [Fact(DisplayName = "Cache: a valid cached entry should be retreived")]
        public void AddEntryToCach_WithResonableSize_ShouleBeFoundBeKey()
        {
            //Arrange
            this.Initialize();

            var targetedValue = Guid.NewGuid().ToString();

            var user = new User()
            {
                Id = 25,
                Name = targetedValue
            };

            //Act
            _cache[user.Id] = user;
            var result = _cache[user.Id];

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result?.Name, targetedValue);
        }

        [Fact(DisplayName = "Caceh : a too big cache entry not cached")]
        public void AddEntryToCache_WithSizeBiggerThanCacheLimit_ShouldnotBeCached()
        {
            //Arrange
            this.Initialize();

            //Act
            var user = new User()
            {
                Id = 25,
                Name = new string('A', this._sizeLimit + 1)
            };

            _cache[user.Id] = user;
            var result = _cache[user.Id];

            //Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "Cache: once cleared, existed entry should be removed")]
        public void Cache_OnceClearExecuted_EntriesShouldDeleted()
        {
            //Arrange
            this.Initialize();

            var user = new User()
            {
                Id = 25,
                Name = Guid.NewGuid().ToString()
            };

            //Act
            _cache[user.Id] = user;
            _cache.Clear();

            var result = _cache[user.Id];

            //Assert
            Assert.Null(result);
        }
    }
}

using Microsoft.Extensions.Caching.Distributed;

namespace BookShop.WebUi.Services
{
    public interface IRedisService
    {
        public Task SetObjectAsync<T>(IDistributedCache cache,string key, T value, double lifeSpan = 14400,bool sliding = false);

        public Task<T> GetObjectAsync<T>(IDistributedCache cache,string key);
    }
}

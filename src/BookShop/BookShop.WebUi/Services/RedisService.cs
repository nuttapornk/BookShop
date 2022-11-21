using Hyperion;
using Microsoft.Extensions.Caching.Distributed;

namespace BookShop.WebUi.Services
{
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache _cache;
        private readonly Serializer _serializer;
        public RedisService(IDistributedCache cache)
        {
            _cache = cache; 
            _serializer = new Serializer(new SerializerOptions(preserveObjectReferences:true));
        }

        public async Task SetObjectAsync<T>(IDistributedCache cache, string key, T value, double lifeSpan = 14400, bool sliding = false)
        {
            await using var mem = new MemoryStream();
            _serializer.Serialize(value, mem);
            var toWrite = mem.ToArray();
            DistributedCacheEntryOptions options = new();

            if (sliding)
                options.SlidingExpiration = System.TimeSpan.FromSeconds(lifeSpan);
            else
                options.AbsoluteExpirationRelativeToNow = System.TimeSpan.FromSeconds(lifeSpan);

            await _cache.SetAsync(key,toWrite,options);
        }

        public async Task<T> GetObjectAsync<T>(IDistributedCache cache, string key)
        {
            var bytes = await _cache.GetAsync(key);
            if (bytes == null) return default;
            await using var mem = new MemoryStream(bytes);
            return _serializer.Deserialize<T>(mem);
        }
    }
}

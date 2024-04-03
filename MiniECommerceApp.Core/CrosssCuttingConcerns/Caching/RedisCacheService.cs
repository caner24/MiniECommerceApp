using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiniECommerceApp.Core.CrosssCuttingConcerns.Caching
{
    public class RedisCacheService
    {
        private readonly IDistributedCache? _cache;
        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }
        public T GetCachedData<T>(string key)
        {
            var jsonData = _cache.GetString(key);
            if (jsonData == null)
                return default(T);
            return JsonSerializer.Deserialize<T>(jsonData);
        }

        public void SetCachedData<T>(string key, T data, TimeSpan cacheDuration)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = cacheDuration,
                SlidingExpiration = cacheDuration
            };
            JsonSerializerOptions jsonSerilazeOptions = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            var jsonData = JsonSerializer.Serialize(data, jsonSerilazeOptions);
            _cache.SetString(key, jsonData, options);
        }
    }
}

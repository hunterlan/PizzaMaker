using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace PizzaMaker.Presentation.Extensions;

public static class DistributedCacheExtension
{
    public static async Task SetAsync<T>(this IDistributedCache distributedCache, string key, T value,
        TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromHours(1),
            SlidingExpiration = unusedExpireTime
        };

        var jsonData = JsonSerializer.Serialize(value);
        await distributedCache.SetStringAsync(key, jsonData, options);
    }

    public static async Task<T?> GetAsync<T>(this IDistributedCache distributedCache, string key)
    {
        var jsonData = await distributedCache.GetStringAsync(key);

        if (jsonData == null)
        {
            return default(T);
        }
        
        return JsonSerializer.Deserialize<T>(jsonData);
    }
}
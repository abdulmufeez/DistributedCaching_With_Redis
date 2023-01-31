using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CachingWithRedis.Extensions
{
    public static class DistributedCachingExtension
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache, string recordKey, T recordData)
        {
            var absoluteExpiration = AppSettingsExtension.GetValue<int>("CacheSettings:AbsoluteExpiration");
            var unusedExpiraion = AppSettingsExtension.GetValue<int>("CacheSettings:UnusedExpiraion");

            if (!string.IsNullOrEmpty(recordKey) && recordData is not null)
            {
                var options = new DistributedCacheEntryOptions();

                // this mean when data will be removed         
                options.AbsoluteExpirationRelativeToNow = absoluteExpiration.Equals(0) ? null : TimeSpan.FromDays(absoluteExpiration);
                // this mean if unactive then when will data removed
                options.SlidingExpiration = unusedExpiraion.Equals(0) ? null : TimeSpan.FromHours(unusedExpiraion);

                var jsonData = JsonConvert.SerializeObject(recordData);

                // saving data in cache as string although it redise supports many different data types
                await cache.SetStringAsync(recordKey, jsonData, options);
            }
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordKey)
        {
            // fetching data from cache
            var jsonData = await cache.GetStringAsync(recordKey);

            if (!string.IsNullOrEmpty(jsonData))
                return JsonConvert.DeserializeObject<T>(jsonData);

            return default(T);
        }

        public static async Task RemoveRecordAsync<T>(this IDistributedCache cache, string recordKey)
        {
            await cache.RemoveAsync(recordKey);            
        }
    }
}

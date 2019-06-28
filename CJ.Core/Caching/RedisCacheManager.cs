using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CJ.Core.Caching
{
    public class RedisCacheManager:ICacheManager
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCacheManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public string Get<T>(string key)
        {
            return GetAsync(key).Result;
        }

        public async void Set<T>(string key, T value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            await SetAsync(key, value, new DistributedCacheEntryOptions());
        }

        public async void Refresh(string key)
        {
            await RefreshAsync(key);
        }

        public async void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            await RemoveAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options,
            CancellationToken token = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            
            var jsonValue = JsonConvert.SerializeObject(value);
            await _distributedCache.SetStringAsync(key, jsonValue, options, token);
        }

        public async Task<string> GetAsync(string key, CancellationToken token = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            token.ThrowIfCancellationRequested();
            return await _distributedCache.GetStringAsync(key, token);
        }

        public async Task RemoveAsync(string key, CancellationToken token = default(CancellationToken))
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            await _distributedCache.RemoveAsync(key, token);
        }
        public async Task RefreshAsync(string key, CancellationToken token = default(CancellationToken))
        {
            await _distributedCache.RefreshAsync(key, token);
        }
    }
}
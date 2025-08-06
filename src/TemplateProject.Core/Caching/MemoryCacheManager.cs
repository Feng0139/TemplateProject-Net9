using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Memory;

namespace TemplateProject.Core.Caching;

public class MemoryCacheManager(IMemoryCache memoryCache) : ICacheManager
{
    public Task<T?> Get<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        memoryCache.TryGetValue<T>(key, out var result);

        return Task.FromResult(result);
    }

    public Task Set(string key, object data, TimeSpan? expiry = null, CancellationToken cancellationToken = default)
    {
        if (expiry == null)
        {
            memoryCache.Set(key, data);
        }
        else
        {
            memoryCache.Set(key, data, expiry.Value);
        }

        return Task.CompletedTask;
    }

    public Task Remove(string key, CancellationToken cancellationToken = default)
    {
        memoryCache.Remove(key);

        return Task.CompletedTask;
    }

    public Task RemoveByPrefix(string pattern, CancellationToken cancellationToken = default)
    {
        foreach (var key in GetKeys(memoryCache, pattern))
        {
            memoryCache.Remove(key);
        }

        return Task.CompletedTask;
    }

    public Task Clear(CancellationToken cancellationToken = default)
    {
        foreach (var key in GetKeys(memoryCache))
        {
            memoryCache.Remove(key);
        }
        
        return Task.CompletedTask;
    }

    public void Dispose() { }

    public static List<string> GetKeys(IMemoryCache memoryCache, string pattern = "")
    {
        var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
        var keys = new List<string>();
        
        if (memoryCache is MemoryCache cache)
        {
            keys.AddRange(cache.Keys
                .OfType<string>()
                .Where(key => string.IsNullOrEmpty(pattern) || regex.IsMatch(key)));
        }

        return keys;
    }
}
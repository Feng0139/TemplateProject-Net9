using StackExchange.Redis;
using TemplateProject.Core.Extension;

namespace TemplateProject.Core.Caching
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly IDatabase _database;

        public RedisCacheManager(IConnectionMultiplexer redisConnection, int redisDatabaseIndex)
        {
            _redisConnection = redisConnection;
            _database = redisConnection.GetDatabase(redisDatabaseIndex);
        }

        public async Task<T?> Get<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            var redisValue = await _database.StringGetAsync(key);
            return redisValue.HasValue ? redisValue.ToString().DeserializeJson<T>() : null;
        }

        public async Task Set(string key, object data, TimeSpan? expiry = null, CancellationToken cancellationToken = default)
        {
            await _database.StringSetAsync(key, data.SerializeJson(), expiry);
        }

        public async Task Remove(string key, CancellationToken cancellationToken = default)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task RemoveByPrefix(string pattern, CancellationToken cancellationToken = default)
        {
            var endpoints = _redisConnection.GetEndPoints();
            var server = _redisConnection.GetServer(endpoints[0]);

            var keys = server.Keys(pattern: pattern).ToList();
            if(keys.Count != 0)
            {
                await _database.KeyDeleteAsync(keys.ToArray());
            }
        }

        public async Task Clear(CancellationToken cancellationToken = default)
        {
            var endpoints = _redisConnection.GetEndPoints();
            var server = _redisConnection.GetServer(endpoints[0]);

            var keys = server.Keys().ToList();
            if(keys.Count != 0)
            {
                await _database.KeyDeleteAsync(keys.ToArray());
            }
        }

        public void Dispose()
        {
            _redisConnection.Dispose();
        }
    }
}
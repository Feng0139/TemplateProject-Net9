using Microsoft.Extensions.Configuration;
using TemplateProject.Message.Enum;

namespace TemplateProject.Core.Settings.System
{
    public class CacheSetting : IConfigurationSetting
    {
        public CacheTypeEnum UseStorage {  get; }
        
        public string RedisConnection { get; }
        
        public int RedisDatabaseIndex { get; }

        public CacheSetting(IConfiguration configuration)
        {
            UseStorage = configuration.GetValue<CacheTypeEnum>("Cache:UseStorage");
            RedisConnection = configuration["ConnectionStrings:Redis"] ?? string.Empty;
            RedisDatabaseIndex = configuration.GetValue<int?>("Cache:RedisDatabaseIndex") ?? 0;
        }
    }
}

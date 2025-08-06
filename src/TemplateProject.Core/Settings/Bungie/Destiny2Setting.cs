using Microsoft.Extensions.Configuration;

namespace TemplateProject.Core.Settings.Bungie;

public class Destiny2Setting : IConfigurationSetting
{
    public List<string> SyncLanguages { get; }
    
    public List<string> SyncManifests { get; }
    
    public Destiny2Setting(IConfiguration configuration)
    {
        SyncLanguages = configuration.GetSection("Destiny2:SyncLanguages").Get<List<string>>() ?? [];
        SyncManifests = configuration.GetSection("Destiny2:SyncManifests").Get<List<string>>() ?? [];
    }
}
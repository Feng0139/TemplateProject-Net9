using Microsoft.Extensions.Configuration;

namespace TemplateProject.Core.Settings.Steam;

public class SteamSetting : IConfigurationSetting
{
    public string ApiKey { get; set; }
    
    public SteamSetting(IConfiguration configuration)
    {
        ApiKey = configuration["Steam:ApiKey"] ?? string.Empty;
    }
}
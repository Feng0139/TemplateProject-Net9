using Microsoft.Extensions.Configuration;

namespace TemplateProject.Core.Settings.Bungie;

public class BungieSetting : IConfigurationSetting
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string ApiKey { get; set; }
    
    public BungieSetting(IConfiguration configuration)
    {
        ClientId = configuration["Bungie:ClientId"] ?? string.Empty;
        ClientSecret = configuration["Bungie:ClientSecret"] ?? string.Empty;
        ApiKey = configuration["Bungie:ApiKey"] ?? string.Empty;
    }
}
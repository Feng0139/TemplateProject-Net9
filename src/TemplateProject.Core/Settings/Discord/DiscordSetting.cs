using Microsoft.Extensions.Configuration;

namespace TemplateProject.Core.Settings.Discord;

public class DiscordSetting : IConfigurationSetting
{
    public bool EnableProxy { get; set; }
    
    public bool AutoStart { get; set; }
    
    public string Token { get; set; }
    
    public DiscordSetting(IConfiguration configuration)
    {
        EnableProxy = configuration.GetValue<bool>("Discord:EnableProxy");
        AutoStart = configuration.GetValue<bool>("Discord:AutoStart");
        Token = configuration["Discord:Token"] ?? string.Empty;
    }
}
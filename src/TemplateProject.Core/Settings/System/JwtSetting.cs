using Microsoft.Extensions.Configuration;

namespace TemplateProject.Core.Settings.System;

public class JwtSetting : IConfigurationSetting
{
    public string Secret { get; }
    
    public string Issuer { get; }
    
    public string Audience { get; }

    public int ExpireMinutes { get; }

    public JwtSetting(IConfiguration configuration)
    {
        Secret = configuration["Jwt:Secret"] ?? string.Empty;
        
        Issuer = configuration["Jwt:Issuer"] ?? string.Empty;
        
        Audience = configuration["Jwt:Audience"] ?? string.Empty;
        
        ExpireMinutes = configuration.GetValue<int>("Jwt:ExpireMinutes");
    }
}
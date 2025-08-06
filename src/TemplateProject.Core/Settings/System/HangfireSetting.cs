using Microsoft.Extensions.Configuration;
using TemplateProject.Message.Enum;

namespace TemplateProject.Core.Settings.System;

public class HangfireSetting : IConfigurationSetting
{
    public bool EnableDashboard { get; }
    
    public string DashboardPath { get; }
    
    public string DashboardUser { get; }
    
    public string DashboardPassword { get; }
    
    public CacheTypeEnum UseStorage { get; }

    public HangfireSetting(IConfiguration configuration)
    {
        EnableDashboard = configuration.GetValue<bool>("Hangfire:EnableDashboard");
        
        DashboardPath = configuration["Hangfire:DashboardPath"] ?? "/hangfire";
        
        DashboardUser = configuration["Hangfire:DashboardUser"] ?? "admin";
        
        DashboardPassword = configuration["Hangfire:DashboardPassword"] ?? "hangfire";
        
        UseStorage = configuration.GetValue<CacheTypeEnum>("Cache:UseStorage");
    }
}
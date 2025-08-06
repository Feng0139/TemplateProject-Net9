namespace TemplateProject.Core.Settings;

public class AppSetting
{
    public static string SecretKey { get; set; } = string.Empty;

    public static bool EnableScalar { get; set; }
    
    public static string DomainUrl { get; set; } = string.Empty;
    
    public static string ProxyUrl { get; set; } = string.Empty;
}
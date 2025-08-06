using System.ComponentModel;

namespace TemplateProject.Message.Enum;

public enum AppPlatformEnum
{
    [Description("Unknown")]
    Unknown = 0,
    
    [Description("Self")]
    Self = 1 << 0
}
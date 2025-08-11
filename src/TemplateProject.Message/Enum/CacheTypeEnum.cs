using System.ComponentModel;

namespace TemplateProject.Message.Enum
{
    public enum CacheTypeEnum
    {
        [Description("Memory")]
        Memory = 0,
    
        [Description("Redis")]
        Redis = 1
    }
}
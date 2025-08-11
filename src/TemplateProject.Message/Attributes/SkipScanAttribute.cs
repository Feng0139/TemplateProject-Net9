namespace TemplateProject.Message.Attributes;

/// <summary>
/// 跳过扫描特性
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class SkipScanAttribute : Attribute
{
    
}
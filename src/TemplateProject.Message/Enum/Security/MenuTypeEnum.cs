namespace TemplateProject.Message.Enum.Security;

/// <summary>
/// 菜单类型枚举
/// </summary>
public enum MenuTypeEnum
{
    /// <summary>
    /// 文件夹, 仅用于分组
    /// </summary>
    Directory = 0,
    
    /// <summary>
    /// 页面, 拥有可展示内容
    /// </summary>
    Page = 1,
    
    /// <summary>
    /// 按钮
    /// </summary>
    Button = 2
}
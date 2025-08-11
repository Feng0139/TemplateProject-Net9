namespace TemplateProject.Message.Dto.Security;

public class PermissionMenuDto
{
    public Guid Id { get; set; }

    public Guid PermissionId { get; set; }

    public Guid MenuId { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset? UpdatedAt { get; set; }
    
    public Guid CreatedBy { get; set; }
    
    public Guid? LastModifiedBy { get; set; }
}
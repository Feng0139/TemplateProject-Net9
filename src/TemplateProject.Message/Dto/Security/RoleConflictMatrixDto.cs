namespace TemplateProject.Message.Dto.Security;

public class RoleConflictMatrixDto
{
    public Guid Id { get; set; }
    
    public Guid RoleAId { get; set; }

    public Guid RoleBId { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset? UpdatedAt { get; set; }
    
    public Guid CreatedBy { get; set; }
    
    public Guid? LastModifiedBy { get; set; }
}
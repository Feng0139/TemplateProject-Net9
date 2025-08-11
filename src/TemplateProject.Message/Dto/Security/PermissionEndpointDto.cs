namespace TemplateProject.Message.Dto.Security;

public class PermissionEndpointDto
{
    public Guid Id { get; set; }
    
    public Guid PermissionId { get; set; }
    
    public string Endpoint { get; set; } = string.Empty;
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset? UpdatedAt { get; set; }
    
    public Guid CreatedBy { get; set; }
    
    public Guid? LastModifiedBy { get; set; }
}
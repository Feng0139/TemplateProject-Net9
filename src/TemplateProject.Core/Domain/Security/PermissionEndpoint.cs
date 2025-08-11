using System.ComponentModel.DataAnnotations.Schema;
using TemplateProject.Message.Dto;

namespace TemplateProject.Core.Domain.Security;

[Table("Permission_Endpoint")]
public class PermissionEndpoint : IEntityAudit<Guid>
{
    public Guid Id { get; set; }
    
    public Guid PermissionId { get; set; }
    
    public string Endpoint { get; set; } = string.Empty;
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset? UpdatedAt { get; set; }
    
    public Guid CreatedBy { get; set; }
    
    public Guid? LastModifiedBy { get; set; }
}
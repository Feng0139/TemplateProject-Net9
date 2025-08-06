using System.ComponentModel.DataAnnotations.Schema;
using TemplateProject.Message.Dto;

namespace TemplateProject.Core.Domain.Security;

[Table("Role_Conflict_Matrix")]
public class RoleConflictMatrix : IEntityAudit<Guid>
{
    public Guid Id { get; set; }
    
    public Guid RoleAId { get; set; }

    public Guid RoleBId { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset? UpdatedAt { get; set; }
    
    public Guid CreatedBy { get; set; }
    
    public Guid? LastModifiedBy { get; set; }
}
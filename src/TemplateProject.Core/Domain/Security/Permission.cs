using System.ComponentModel.DataAnnotations.Schema;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Enum;
using TemplateProject.Message.Enum.Security;

namespace TemplateProject.Core.Domain.Security;

[Table("Permission")]
public class Permission : ITreeEntityAudit<Guid, Permission>
{
    public Guid Id { get; set; }
    
    public Guid Pid { get; set; }

    [NotMapped]
    public List<Permission> Children { get; set; } = [];

    public PermissionTypeEnum Type { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;
    
    public int Sequence { get; set; }

    public StatusEnum Status { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset? UpdatedAt { get; set; }
    
    public Guid CreatedBy { get; set; }
    
    public Guid? LastModifiedBy { get; set; }
}
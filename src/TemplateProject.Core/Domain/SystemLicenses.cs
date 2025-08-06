using System.ComponentModel.DataAnnotations.Schema;
using TemplateProject.Message.Dto;

namespace TemplateProject.Core.Domain;

[Table("system_licenses")]
public class SystemLicenses : IEntityAudit<Guid>
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("api_key")]
    public string ApiKey { get; set; } = string.Empty;
    
    [Column("user_name")]
    public string UserName { get; set; } = string.Empty;
    
    public Guid CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    
    public Guid? LastModifiedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TemplateProject.Core.Extension;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Enum.Account;

namespace TemplateProject.Core.Domain.Account;

[Table("user_account")]
public class UserAccount : IEntity<Guid>, IEntityCreated, IEntityModified
{
    [Key] [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("name")]
    [StringLength(64)]
    [Required]
    public string Name { get; set; } = string.Empty;

    [Column("Suffix")]
    [StringLength(4)]
    public string Suffix { get; set; } = "".GenerateNumberSuffix();
    
    [NotMapped]
    public string DisplayName => $"{Name}#{Suffix}";

    [Column("password")]
    [StringLength(255)]
    [Required]
    public string Password { get; set; } = string.Empty;

    [Column("level")]
    public AccountLevelEnum Level { get; set; } = AccountLevelEnum.User;
    
    [Column("source")]
    public AccountSourceEnum Source { get; set; } = AccountSourceEnum.Local;
    
    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

    [Column("update_at")]
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
    
    [NotMapped]
    public List<UserThirdParty> ThirdParties { get; set; } = new();
}
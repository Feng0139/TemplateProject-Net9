using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TemplateProject.Message.Dto;

namespace TemplateProject.Core.Domain.Account;

[Table("user_third_party")]
public class UserThirdParty : IEntity<Guid>, IEntityCreated, IEntityModified
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("provider")]
    [Required]
    public string Provider { get; set; } = string.Empty;
    
    [Column("third_party_user_id")]
    [StringLength(255)]
    [Required]
    public string ThirdPartyUserId { get; set; } = string.Empty;

    [Column("access_token")]
    [StringLength(512)]
    public string AccessToken { get; set; } = string.Empty;

    [Column("access_token_expires_at")]
    public DateTimeOffset? AccessTokenExpiresAt { get; set; }

    [Column("refresh_token")]
    [StringLength(512)]
    public string RefreshToken { get; set; } = string.Empty;

    [Column("refresh_token_expires_at")]
    public DateTimeOffset? RefreshTokenExpiresAt { get; set; }

    [Column("extra_data")]
    [StringLength(2048)]
    public string ExtraData { get; set; } = string.Empty;
    
    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [Column("update_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
}
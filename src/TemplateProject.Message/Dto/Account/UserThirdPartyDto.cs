namespace TemplateProject.Message.Dto.Account;

public class UserThirdPartyDto
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }

    public string Provider { get; set; } = string.Empty;
    
    public string ThirdPartyUserId { get; set; } = string.Empty;

    public string AccessToken { get; set; } = string.Empty;

    public DateTimeOffset? AccessTokenExpiresAt { get; set; }
    
    public string RefreshToken { get; set; } = string.Empty;
    
    public DateTimeOffset? RefreshTokenExpiresAt { get; set; }
    
    public string ExtraData { get; set; } = string.Empty;
    
    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}
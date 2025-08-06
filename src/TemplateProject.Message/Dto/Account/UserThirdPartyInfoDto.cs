namespace TemplateProject.Message.Dto.Account;

public class UserThirdPartyInfoDto
{
    public string Provider { get; set; } = string.Empty;
    
    public string ThirdPartyUserId { get; set; } = string.Empty;
    
    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}
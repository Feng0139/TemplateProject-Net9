using TemplateProject.Message.Enum.Account;

namespace TemplateProject.Message.Dto.Account;

public class UserAccountInfoDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string Suffix { get; set; } = string.Empty;
    
    public string DisplayName => $"{Name}#{Suffix}";
    
    public AccountLevelEnum Level { get; set; }
    
    public AccountSourceEnum Source { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
    
    public List<UserThirdPartyInfoDto> ThirdParties { get; set; } = new();
}
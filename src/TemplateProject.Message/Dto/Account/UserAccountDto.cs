using TemplateProject.Message.Enum.Account;

namespace TemplateProject.Message.Dto.Account;

public class UserAccountDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Suffix { get; set; } = string.Empty;
    
    public string DisplayName => $"{Name}#{Suffix}";
    
    public string Password { get; set; } = string.Empty;
    
    public AccountLevelEnum Level { get; set; } = AccountLevelEnum.User;
    
    public AccountSourceEnum Source { get; set; } = AccountSourceEnum.Local;
    
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
    
    public List<UserThirdPartyDto> ThirdParties { get; set; } = new();
}
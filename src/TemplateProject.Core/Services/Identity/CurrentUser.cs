using TemplateProject.Message.Enum.Account;

namespace TemplateProject.Core.Services.Identity;

public interface ICurrentUser
{
    Guid? Id { get; }
    
    string UserName { get; }
    
    AccountLevelEnum Level { get; }

    AccountSourceEnum Source { get; }
}

public class CurrentUserProxy(ICurrentUserProvider provider) : ICurrentUser
{
    public Guid? Id => provider.CurrentUser?.Id;

    public string UserName => provider.CurrentUser?.UserName ?? string.Empty;

    public AccountLevelEnum Level => provider.CurrentUser?.Level ?? AccountLevelEnum.User;
    
    public AccountSourceEnum Source => provider.CurrentUser?.Source ?? AccountSourceEnum.Local;
}
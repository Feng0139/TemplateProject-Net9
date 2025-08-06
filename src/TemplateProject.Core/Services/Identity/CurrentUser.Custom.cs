using TemplateProject.Message.Enum.Account;

namespace TemplateProject.Core.Services.Identity;

public class CustomCurrentUser : ICurrentUser
{
    public Guid? Id { get; }

    public string UserName { get; } = string.Empty;
    
    public AccountLevelEnum Level { get; }
    
    public AccountSourceEnum Source { get; }

    public CustomCurrentUser()
    {
    }
    
    public CustomCurrentUser(Guid? id, string userName, AccountLevelEnum level, AccountSourceEnum source)
    {
        Id = id;
        UserName = userName;
        Level = level;
        Source = source;
    }
}
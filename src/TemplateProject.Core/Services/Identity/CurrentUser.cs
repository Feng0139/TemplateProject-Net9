namespace TemplateProject.Core.Services.Identity;

public interface ICurrentUser
{
    Guid? Id { get; }
    
    string UserName { get; }
}

public class CurrentUserProxy(ICurrentUserProvider provider) : ICurrentUser
{
    public Guid? Id => provider.CurrentUser?.Id;

    public string UserName => provider.CurrentUser?.UserName ?? string.Empty;
}
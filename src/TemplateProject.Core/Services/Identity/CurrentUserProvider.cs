namespace TemplateProject.Core.Services.Identity;

public interface ICurrentUserProvider : IScope
{
    ICurrentUser? CurrentUser { get; }
    
    void SetCurrentUser(ICurrentUser currentUser);
}

public class CurrentUserProvider : ICurrentUserProvider
{
    public ICurrentUser? CurrentUser { get; private set; }

    public void SetCurrentUser(ICurrentUser currentUser)
    {
        CurrentUser = currentUser;
    }
}
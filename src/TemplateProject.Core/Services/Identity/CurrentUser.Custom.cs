namespace TemplateProject.Core.Services.Identity;

public class CustomCurrentUser : ICurrentUser
{
    public Guid? Id { get; }

    public string UserName { get; } = string.Empty;
    
    public CustomCurrentUser()
    {
    }
    
    public CustomCurrentUser(Guid? id, string userName)
    {
        Id = id;
        UserName = userName;
    }
}
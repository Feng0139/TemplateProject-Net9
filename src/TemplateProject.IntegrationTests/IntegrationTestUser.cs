using TemplateProject.Core.Services.Identity;

namespace TemplateProject.IntegrationTest;

public class IntegrationTestUser  : ICurrentUser
{
    public Guid? Id => Guid.Parse("33333333-3333-3333-3333-333333333333");
    
    public string UserName => "IntegrationTestUser";
}
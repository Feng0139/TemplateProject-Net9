using Shouldly;
using TemplateProject.Core.Extension;
using TemplateProject.Core.Services.Account;
using TemplateProject.Core.Services.Identity;
using TemplateProject.Message.Commands.Account;
using TemplateProject.Message.Request.Identity;
using TestStack.BDDfy;

namespace TemplateProject.IntegrationTest.BDD.Identity;

public class RegisterAndLoginUserScenario(IntegrationTestBase testBase) : IntegrationFixture(testBase)
{
    private string _userName { get; set; } = "test";
    private string _password { get; set; } = "".GenerateRandomString();
    private string _suffix { get; set; } = string.Empty;
    private string _accessToken { get; set; } = string.Empty;
    private string _refreshToken { get; set; } = string.Empty;
    
    public async Task GivenRegister()
    {
        await RunWithUnitOfWork<IAccountService>(async service =>
        {
            var response = await service.RegisterUserAccountAsync(
                new RegisterUserAccountCommand
                {
                    Name = _userName,
                    Password = _password
                }, CancellationToken.None).ConfigureAwait(false);

            if (response is { Data: not null })
            {
                _suffix = response.Data.Suffix;
            }
        });
    }
    
    public async Task WhenLogin()
    {
        await Run<IIdentityService>(async service =>
        {
            var response = await service.AuthenticateAsync(
                new AuthenticateRequest
                {
                    Name = _userName,
                    Password = _password,
                    Suffix = _suffix
                }, CancellationToken.None).ConfigureAwait(false);
            
            if (response is { Data: not null })
            {
                _accessToken = response.Data.AccessToken;
                _refreshToken = response.Data.RefreshToken;
            }
        });
    }

    public async Task ThenLogged()
    {
        _accessToken.ShouldNotBeNullOrEmpty();
        _refreshToken.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public void Execute()
    {
        this.Given(x => x.GivenRegister())
            .When(x => x.WhenLogin())
            .Then(x => x.ThenLogged())
            .BDDfy();
    }
}
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TemplateProject.Core.Data;
using TemplateProject.Core.Domain;
using TemplateProject.Core.Services.Identity;

namespace TemplateProject.Api.Authentication;

public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string AuthenticationScheme = "ApiKey";
    
    public const string SchemeHeader = "x-api-key";
}

public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
{
    private readonly IApiKeyService _service;
    private readonly IRepository<SystemLicenses> _licensesRep;
    
#pragma warning disable CS0618 // Type or member is obsolete
    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<ApiKeyAuthenticationOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock,
        IApiKeyService service, IRepository<SystemLicenses> licensesRep) : base(options, logger, encoder, clock)
    {
        _service = service;
        _licensesRep = licensesRep;
    }
#pragma warning restore CS0618 // Type or member is obsolete

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(ApiKeyAuthenticationOptions.SchemeHeader, out var apiKey))
            return AuthenticateResult.NoResult();
        
        if (apiKey.Count == 0)
            return AuthenticateResult.Fail("No ApiKey provided.");
        
        var decryptedApiKey = string.Empty;
        
        try
        {
            decryptedApiKey = _service.Decrypt(apiKey!);
        }
        catch (Exception)
        {
            return AuthenticateResult.Fail("Invalid ApiKey.");
        }
        
        var findApiKey = await _licensesRep.QueryNoTracking()
            .FirstOrDefaultAsync(x => x.ApiKey == decryptedApiKey).ConfigureAwait(false);
        
        if (findApiKey is null)
            return AuthenticateResult.Fail("Invalid ApiKey.");
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, findApiKey.UserName),
            new Claim(ClaimTypes.NameIdentifier, findApiKey.CreatedBy.ToString())
        };
        var identity = new ClaimsIdentity(claims, nameof(ApiKeyAuthenticationHandler));
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
            
        return AuthenticateResult.Success(ticket);
    }
}
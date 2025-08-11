using System.Net;
using System.Security.Claims;
using Serilog;
using TemplateProject.Core.Services.Account;
using TemplateProject.Message.Dto.Account;
using TemplateProject.Message.Dto.Identity;
using TemplateProject.Message.Request.Account;
using TemplateProject.Message.Request.Identity;

namespace TemplateProject.Core.Services.Identity;

public interface IIdentityService : IScope
{
    Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request, CancellationToken cancellationToken);
}

public class IdentityService : IIdentityService
{
    private readonly ILogger _logger;
    private readonly ITokenClaimService _tokenClaimService;
    private readonly IAccountService _accountService;

    public IdentityService(ILogger logger, ITokenClaimService tokenClaimService, IAccountService accountService)
    {
        _logger = logger;
        _tokenClaimService = tokenClaimService;
        _accountService = accountService;
    }

    private List<Claim> GetUserAllClaimsAsync(UserAccountInfoDto user, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.DisplayName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("account_level", user.Level.ToString()),
            new Claim("account_source", user.Source.ToString())
        };
        
        return claims;
    }
    
    public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request, CancellationToken cancellationToken)
    {
        var response = new AuthenticateResponse();
        
        var getUserAccountResponse = await _accountService.GetUserAccountAsync(
            new GetUserAccountRequest
            {
                DisplayName = $"{request.Name}#{request.Suffix}"
            }, cancellationToken).ConfigureAwait(false);

        if (getUserAccountResponse.Data == null)
        {
            response.Code = HttpStatusCode.InternalServerError;
            response.Message = "用户不存在";
            return response;
        }
        
        var userAccount = getUserAccountResponse.Data;
        
        var result = await _accountService.VerifyPassword(userAccount.Id, request.Password, cancellationToken).ConfigureAwait(false);
        if (!result)
        {
            response.Code = HttpStatusCode.InternalServerError;
            response.Message = "密码错误";
            return response;
        }
        
        var accessToken = _tokenClaimService.GenerateAccessToken(GetUserAllClaimsAsync(userAccount, cancellationToken));
        var refreshToken = _tokenClaimService.GenerateRefreshToken();
        
        response.Data = new JwtToken
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
        return response;
    }
}
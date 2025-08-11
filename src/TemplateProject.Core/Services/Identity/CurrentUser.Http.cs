using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TemplateProject.Message.Enum.Account;

namespace TemplateProject.Core.Services.Identity;

public class HttpCurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpCurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? Id
    {
        get
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return null;
            }

            var idClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            return Guid.TryParse(idClaim?.Value, out var id) ? id : null;
        }
    }

    public string UserName
    {
        get
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return string.Empty;
            }

            var userNameClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);

            return userNameClaim?.Value ?? string.Empty;
        }
    }

    public AccountLevelEnum Level
    {
        get
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return AccountLevelEnum.User;
            }

            var userLevelClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "account_level");
            
            return Enum.TryParse<AccountLevelEnum>(userLevelClaim?.Value, out var level)
                ? level
                : AccountLevelEnum.User;
        }
    }

    public AccountSourceEnum Source
    {
        get
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return AccountSourceEnum.Local;
            }
            
            var sourceClaim = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "account_source");

            return Enum.TryParse<AccountSourceEnum>(sourceClaim?.Value, out var source)
                ? source
                : AccountSourceEnum.Local;
        }
    }
}
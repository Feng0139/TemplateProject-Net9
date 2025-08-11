using System.Security.Claims;
using Microsoft.AspNetCore.Http;

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
}
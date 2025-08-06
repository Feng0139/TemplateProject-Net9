using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TemplateProject.Core.Services.Identity;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Enum.Account;

namespace TemplateProject.Api.Filters;

public class SecurityAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var currentUser = context.HttpContext.RequestServices.GetService<ICurrentUser>();
        if (currentUser?.Id == null || currentUser.Id == Guid.Empty || currentUser.Level == AccountLevelEnum.SuperAdmin)
        {
            return;
        }
        
        var path = context.HttpContext.Request.Path.Value?.ToLower() ?? string.Empty;
        var method = context.HttpContext.Request.Method.ToUpper();
        
        var securityService = context.HttpContext.RequestServices.GetService<ISecurityService>()!;
        
        var isAllow = await securityService.VerifyPermissionEndpointAsync(
            currentUser.Id.Value, path, method, context.HttpContext.RequestAborted).ConfigureAwait(false);
        
        if (!isAllow)
        {
            var response = new TemplateProjectResponse
            {
                Code = HttpStatusCode.Forbidden,
                Message = "You do not have permission to access."
            };
            
            context.Result = new ObjectResult(response);
        }
    }
}
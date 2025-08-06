using Microsoft.AspNetCore.Authentication;
using TemplateProject.Api.Authentication;

namespace TemplateProject.Api.Extension;

public static class ApiKeyExtensions
{
    public static AuthenticationBuilder AddApiKeyScheme(this AuthenticationBuilder builder)
    {
        return builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationOptions.AuthenticationScheme, _ => { });
    }
}
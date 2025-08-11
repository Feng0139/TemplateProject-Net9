using Microsoft.AspNetCore.Authentication;
using TemplateProject.Api.Authentication;

namespace TemplateProject.Api.Extension;

public static class JwtExtensions
{
    public static AuthenticationBuilder AddJwtScheme(this AuthenticationBuilder builder)
    {
        builder.AddScheme<CustomizeJwtAuthenticationOptions, CustomizeJwtAuthenticationHandler>(
            CustomizeJwtAuthenticationOptions.AuthenticationScheme, _ => { });
        
        return builder;
    }
}
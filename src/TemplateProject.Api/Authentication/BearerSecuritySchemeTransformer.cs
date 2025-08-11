using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace TemplateProject.Api.Authentication;

public class BearerSecuritySchemeTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider) : IOpenApiDocumentTransformer
{
    private const string SchemeName = "Bearer";
    private const string SchemeHeader = "bearer";
    
    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();
        if (authenticationSchemes.Any(authScheme => authScheme.Name == SchemeName))
        {
            document.Components ??= new OpenApiComponents();
            document.Components.SecuritySchemes.TryAdd(SchemeName, new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = SchemeHeader,
                In = ParameterLocation.Header,
                BearerFormat = "Json Web Token"
            });
            
            // Apply it as a requirement for all operations
            foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations))
            {
                operation.Value.Security.Add(new OpenApiSecurityRequirement
                {
                    [new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Id = SchemeName, Type = ReferenceType.SecurityScheme }
                        }
                    ] = Array.Empty<string>()
                });
            }
        }
    }
}
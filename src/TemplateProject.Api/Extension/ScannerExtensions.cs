using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using TemplateProject.Message.Attributes;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Api.Extension;

public static class ScannerExtensions
{
    public static List<EndpointDto> ScanEndpoints()
    {
        var endpoints = new List<EndpointDto>();
        var controllers = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => typeof(ControllerBase).IsAssignableFrom(type));

        foreach (var controller in controllers)
        {
            if (controller.GetCustomAttribute<SkipScanAttribute>() != null)
                continue;
            
            var controllerName = controller.Name.Replace("Controller", "");

            var controllerRoutes = controller.GetCustomAttributes<RouteAttribute>()
                .Select(attr => attr.Template)
                .DefaultIfEmpty("api/[controller]");

            var methods = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            foreach (var method in methods)
            {
                if (method.GetCustomAttribute<SkipScanAttribute>() != null)
                    continue;
                
                var httpAttrs = method.GetCustomAttributes()
                    .Where(attr => attr is HttpMethodAttribute)
                    .Cast<HttpMethodAttribute>();
                
                if (!httpAttrs.Any()) continue;
                
                var routeAttrs = method.GetCustomAttributes<RouteAttribute>()
                    .Select(attr => attr.Template)
                    .ToList();

                foreach (var httpAttr in httpAttrs)
                {
                    var httpMethod = httpAttr.HttpMethods.FirstOrDefault() ?? "GET";
                    var methodTemplates = new List<string>();

                    // Http*Attribute 通常包含 Template（如 HttpGet("test")）
                    if (!string.IsNullOrEmpty(httpAttr.Template))
                        methodTemplates.Add(httpAttr.Template);

                    // 补充 RouteAttribute 的 Template
                    methodTemplates.AddRange(routeAttrs);

                    if (methodTemplates.Count == 0)
                        methodTemplates.Add(string.Empty); // 无方法路径

                    foreach (var controllerRoute in controllerRoutes)
                    {
                        foreach (var methodTemplate in methodTemplates)
                        {
                            var fullPath = CombineRoutes(controllerRoute, methodTemplate, controllerName);

                            var descAttr = method.GetCustomAttribute<DescriptionAttribute>();
                            var description = descAttr?.Description ?? method.Name;

                            endpoints.Add(new EndpointDto
                            {
                                Controller = controllerName,
                                Endpoint = "/" + fullPath.Trim('/'),
                                Method = httpMethod,
                                Description = description
                            });
                        }
                    }
                }
            }
        }

        return endpoints;
    }
    
    private static string CombineRoutes(string controllerRoute, string methodRoute, string controllerName)
    {
        string baseRoute = controllerRoute.Replace("[controller]", controllerName);

        if (string.IsNullOrWhiteSpace(methodRoute))
            return baseRoute;

        if (methodRoute.StartsWith("~"))
            return methodRoute.TrimStart('~', '/');

        return $"{baseRoute}/{methodRoute}".Replace("//", "/");
    }
}
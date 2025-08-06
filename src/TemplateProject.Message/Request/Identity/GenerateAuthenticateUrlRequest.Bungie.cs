using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Authorization;

namespace TemplateProject.Message.Request.Identity;

public class GenerateAuthenticateUrlFromBungieRequest : IRequest
{
    public required AuthorizationDto Authorization { get; set; }
}

public class GenerateAuthenticateUrlFromBungieResponse : TemplateProjectResponse<string>
{
}
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Authorization;

namespace TemplateProject.Message.Request.Identity;

public class GenerateAuthenticateUrlFromSteamRequest : IRequest
{
    public required AuthorizationDto Authorization { get; set; }
}

public class GenerateAuthenticateUrlFromSteamResponse : TemplateProjectResponse<string>
{
}
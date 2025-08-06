using System.Text.Json.Serialization;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Authorization;

namespace TemplateProject.Message.Commands.Identity;

public class LoginSteamOIDCCommand : ICommand
{
    [JsonPropertyName("openid")]
    public required OpenIDConnectDto OpenId { get; set; }
}

public class LoginSteamCommand : LoginSteamOIDCCommand
{
    public required AuthorizationDto Authorization { get; set; }
}

public class LoginSteamResponse : TemplateProjectResponse<string>
{
}
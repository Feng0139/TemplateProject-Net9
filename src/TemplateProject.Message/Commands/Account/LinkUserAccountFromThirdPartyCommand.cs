using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Authorization;

namespace TemplateProject.Message.Commands.Account;

public class LinkUserAccountFromThirdPartyCommand : ICommand
{
    public required AuthorizationDto Authorization { get; set; }

    public string MasterDisplayName { get; set; } = string.Empty;
    
    public string AccessToken { get; set; } = string.Empty;

    public DateTimeOffset? AccessTokenExpiresAt { get; set; }

    public string RefreshToken { get; set; } = string.Empty;
    
    public DateTimeOffset? RefreshTokenExpiresAt { get; set; }
    
    public string ExtraData { get; set; } = string.Empty;
}

public class LinkUserAccountFromThirdPartyResponse : TemplateProjectResponse
{
}
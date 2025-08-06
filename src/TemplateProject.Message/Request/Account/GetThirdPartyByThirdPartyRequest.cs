using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Account;
using TemplateProject.Message.Enum;

namespace TemplateProject.Message.Request.Account;

public class GetThirdPartyByThirdPartyRequest : IRequest
{
    public AppPlatformEnum CurrentPlatform { get; set; }
    
    [Required]
    public required string ThirdPartyUserId { get; set; }
    
    public AppPlatformEnum TargetPlatform { get; set; }
}

public class GetThirdPartyByThirdPartyResponse : TemplateProjectResponse<UserThirdPartyDto>
{
}
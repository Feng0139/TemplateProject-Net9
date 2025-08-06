using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Account;
using TemplateProject.Message.Enum;

namespace TemplateProject.Message.Request.Account;

public class GetUserAccountByThirdPartyRequest : IRequest
{
    public AppPlatformEnum Platform { get; set; }
    
    public required string ThirdPartyUserId { get; set; }
}

public class GetUserAccountByThirdPartyResponse : TemplateProjectResponse<UserAccountInfoDto>
{
}
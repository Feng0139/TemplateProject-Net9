using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Account;

namespace TemplateProject.Message.Request.Account;

public class GetUserAccountRequest : IRequest
{
    public Guid? UserId { get; set; }

    public string DisplayName { get; set; } = string.Empty; // Tips: Cy#0139
}

public class GetUserAccountResponse : TemplateProjectResponse<UserAccountInfoDto>
{
}
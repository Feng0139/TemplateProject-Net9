using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Message.Request.Security;

public class GetMenuRequest : IRequest
{
    public Guid Id { get; set; }
}

public class GetMenuResponse : TemplateProjectResponse<MenuDto>
{
}
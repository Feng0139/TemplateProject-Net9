using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Message.Request.Security;

public class GetMenuTreeRequest : IRequest
{
}

public class GetMenuTreeResponse : TemplateProjectResponse<List<MenuDto>>
{
}
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Message.Request.Security;

public class GetMenusByPermissionIdRequest : IRequest
{
    public Guid PermissionId { get; set; }
}

public class GetMenusByPermissionIdResponse : TemplateProjectResponse<List<MenuDto>>
{
}
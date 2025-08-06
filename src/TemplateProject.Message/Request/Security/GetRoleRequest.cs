using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Message.Request.Security;

public class GetRoleRequest : IRequest
{
    public Guid Id { get; set; }
}

public class GetRoleResponse : TemplateProjectResponse<RoleDto>
{
}
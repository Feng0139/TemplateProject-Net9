using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Message.Request.Security;

public class GetPermissionsByRoleIdsRequest : IRequest
{
    public List<Guid> RoleIds { get; set; } = [];
}

public class GetPermissionsByRoleIdsResponse : TemplateProjectResponse<List<PermissionDto>>
{
}
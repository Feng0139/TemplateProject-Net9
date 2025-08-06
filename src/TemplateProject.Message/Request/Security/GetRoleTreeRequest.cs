using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Message.Request.Security;

public class GetRoleTreeRequest : IRequest
{
}

public class GetRoleTreeResponse : TemplateProjectResponse<List<RoleDto>>
{
}
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Request.Security;

public class GetRoleHasPermissionRequest : IRequest
{
    public Guid RoleId { get; set; }
    
    public Guid PermissionId { get; set; }
}

public class GetRoleHasPermissionResponse : TemplateProjectResponse<bool>
{
}
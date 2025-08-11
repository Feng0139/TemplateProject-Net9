using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Request.Security;

public class CheckRoleConflictExistsRequest : IRequest
{
    public Guid RoleAId { get; set; }
    
    public Guid RoleBId { get; set; }
}

public class CheckRoleConflictExistsResponse : TemplateProjectResponse<bool>
{
}
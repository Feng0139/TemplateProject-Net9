using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Message.Request.Security;

public class GetRolesByUserIdRequest : IRequest
{
    public Guid UserId { get; set; }
}

public class GetRolesByUserIdResponse : TemplateProjectResponse<GetRolesByUserIdData>
{
}

public class GetRolesByUserIdData
{
    public List<UserRoleDto> UserRoles { get; set; } = [];
    
    public List<RoleDto> Roles { get; set; } = [];
}
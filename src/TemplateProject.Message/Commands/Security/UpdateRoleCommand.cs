using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Message.Commands.Security;

public class UpdateRoleCommand : ICommand
{
    public required RoleDto Role { get; set; }
}

public class UpdateRoleResponse : TemplateProjectResponse
{
}
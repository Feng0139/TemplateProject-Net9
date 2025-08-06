using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Security;

public class ReplaceUserRolesCommand : ICommand
{
    public Guid UserId { get; set; }

    public List<Guid> RoleIds { get; set; } = [];
}

public class ReplaceUserRolesResponse : TemplateProjectResponse
{
}
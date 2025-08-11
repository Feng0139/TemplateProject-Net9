using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Security;

public class ReplacePermissionMenusCommand : ICommand
{
    public Guid PermissionId { get; set; }

    public List<Guid> MenuIds { get; set; } = [];
}

public class ReplacePermissionMenusResponse : TemplateProjectResponse
{
}
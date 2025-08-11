using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Security;

public class ReplacePermissionEndpointsCommand : ICommand
{
    public Guid PermissionId { get; set; }

    public List<string> Endpoints { get; set; } = [];
}

public class ReplacePermissionEndpointsResponse : TemplateProjectResponse
{
}
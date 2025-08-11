using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Message.Commands.Security;

public class UpdatePermissionCommand : ICommand
{
    public required PermissionDto Permission { get; set; }
}

public class UpdatePermissionResponse : TemplateProjectResponse
{
}
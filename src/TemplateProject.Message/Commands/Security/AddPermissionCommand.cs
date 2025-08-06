using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Message.Commands.Security;

public class AddPermissionCommand : ICommand
{
    public PermissionDto Permission { get; set; }
}

public class AddPermissionResponse : TemplateProjectResponse
{
}
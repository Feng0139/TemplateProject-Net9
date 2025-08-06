using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Security;

public class RemoveRoleCascadePermissionCommand : ICommand
{
    [Required]
    [MinLength(1)]
    public List<Guid> RoleIds { get; set; } = [];
}

public class RemoveRoleCascadePermissionResponse : TemplateProjectResponse
{
}
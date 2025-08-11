using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Security;

public class RemovePermissionCascadeRoleCommand : ICommand
{
    [Required]
    [MinLength(1)]
    public List<Guid> PermissionIds { get; set; } = [];
}

public class RemovePermissionCascadeRoleResponse : TemplateProjectResponse
{
}
using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Security;

public class ReplaceRolePermissionsCommand : ICommand
{
    public Guid RoleId { get; set; }

    [Required]
    [MinLength(1)]
    public List<Guid> PermissionIds { get; set; } = [];
}

public class ReplaceRolePermissionsResponse : TemplateProjectResponse
{
}
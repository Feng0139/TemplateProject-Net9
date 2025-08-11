using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Security;

public class RemoveRolesCascadeUserCommand : ICommand
{
    [Required]
    [MinLength(1)]
    public List<Guid> RoleIds { get; set; } = [];
}

public class RemoveRolesCascadeUserResponse : TemplateProjectResponse
{
}
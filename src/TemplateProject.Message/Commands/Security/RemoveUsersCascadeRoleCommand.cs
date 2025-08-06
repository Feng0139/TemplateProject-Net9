using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Security;

public class RemoveUsersCascadeRoleCommand : ICommand
{
    [Required]
    [MinLength(1)]
    public List<Guid> UserIds { get; set; } = [];
}

public class RemoveUsersCascadeRoleResponse : TemplateProjectResponse
{
}
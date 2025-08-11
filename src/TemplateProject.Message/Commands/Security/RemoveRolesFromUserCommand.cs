using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Security;

public class UnassignRolesFromUserCommand : ICommand
{
    public Guid UserId { get; set; }
    
    [Required]
    [MinLength(1)]
    public List<Guid> RoleIds { get; set; } = [];
}

public class UnassignRolesFromUserResponse : TemplateProjectResponse
{
}
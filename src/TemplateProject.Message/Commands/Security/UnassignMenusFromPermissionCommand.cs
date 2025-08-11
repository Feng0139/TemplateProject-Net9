using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Security;

public class UnassignMenusFromPermissionCommand : ICommand
{
    public Guid PermissionId { get; set; }
    
    [Required]
    [MinLength(1)]
    public List<Guid> MenuIds { get; set; } = [];
}

public class UnassignMenusFromPermissionResponse : TemplateProjectResponse
{
}
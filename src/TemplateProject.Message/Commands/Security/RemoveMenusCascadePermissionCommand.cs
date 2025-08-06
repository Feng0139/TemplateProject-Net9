using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Security;

public class RemoveMenusCascadePermissionCommand : ICommand
{
    [Required]
    [MinLength(1)] 
    public List<Guid> MenuIds { get; set; } = [];
}

public class RemoveMenusCascadePermissionResponse : TemplateProjectResponse
{
}
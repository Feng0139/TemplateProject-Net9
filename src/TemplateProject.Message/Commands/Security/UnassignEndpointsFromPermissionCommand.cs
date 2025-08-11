using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Security;

public class UnassignEndpointsFromPermissionCommand : ICommand
{
    public Guid PermissionId { get; set; }

    [Required]
    [MinLength(1)]
    public List<string> Endpoints { get; set; } = [];
}

public class UnassignEndpointsFromPermissionResponse : TemplateProjectResponse
{
}
using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Security;

public class DeleteRoleCascadeCommand : ICommand
{
    [Required]
    [MinLength(1)]
    public List<Guid> IdList { get; set; } = [];
}

public class DeleteRoleCascadeResponse : TemplateProjectResponse
{
}
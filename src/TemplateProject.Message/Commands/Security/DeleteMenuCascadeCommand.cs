using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Security;

public class DeleteMenuCascadeCommand : ICommand
{
    [Required]
    [MinLength(1)]
    public List<Guid> IdList { get; set; } = [];
}

public class DeleteMenuCascadeResponse : TemplateProjectResponse
{
}
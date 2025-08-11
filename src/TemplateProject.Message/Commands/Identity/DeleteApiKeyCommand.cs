using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Identity;

public class DeleteApiKeyCommand : ICommand
{
    [Required]
    [MinLength(1)]
    public required string ApiKey { get; set; }
    
    public Guid? UserId { get; set; }
}

public class DeleteApiKeyResponse : TemplateProjectResponse
{
}
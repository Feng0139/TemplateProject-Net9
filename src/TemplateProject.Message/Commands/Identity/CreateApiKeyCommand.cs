using System.ComponentModel.DataAnnotations;
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Identity;

public class CreateApiKeyCommand : ICommand
{
    [Required]
    [MinLength(1)]
    public string ApiKey { get; set; } = string.Empty;
}

public class CreateApiKeyResponse : TemplateProjectResponse<string>
{
}
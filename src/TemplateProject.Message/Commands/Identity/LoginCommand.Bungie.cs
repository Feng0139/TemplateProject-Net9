using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;

namespace TemplateProject.Message.Commands.Identity;

public class LoginBungieCommand : ICommand
{
    public required string Code { get; set; }
    
    public required string State { get; set; }
}

public class LoginBungieResponse : TemplateProjectResponse<string>
{
}
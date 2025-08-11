using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Message.Commands.Security;

public class UpdateMenuCommand : ICommand
{
    public required MenuDto Menu { get; set; }
}

public class UpdateMenuResponse : TemplateProjectResponse
{
}
using Mediator.Net.Contracts;
using TemplateProject.Message.Dto;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Message.Commands.Security;

public class AddMenuCommand : ICommand
{
    public MenuDto Menu { get; set; }
}

public class AddMenuResponse : TemplateProjectResponse
{
}
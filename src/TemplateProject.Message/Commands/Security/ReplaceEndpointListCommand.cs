using Mediator.Net.Contracts;
using TemplateProject.Message.Dto.Security;

namespace TemplateProject.Message.Commands.Security;

public class ReplaceEndpointListCommand : ICommand
{
    public List<EndpointDto> Endpoints { get; set; } = [];
}
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class ReplaceEndpointListCommandHandler : ICommandHandler<ReplaceEndpointListCommand>
{
    private readonly IEndpointService _service;

    public ReplaceEndpointListCommandHandler(IEndpointService service)
    {
        _service = service;
    }

    public async Task Handle(IReceiveContext<ReplaceEndpointListCommand> context, CancellationToken cancellationToken)
    {
        await _service.ReplaceEndpointListAsync(context.Message).ConfigureAwait(false);
    }
}
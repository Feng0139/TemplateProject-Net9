using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class RemoveEndpointsCascadePermissionCommandHandler : ICommandHandler<RemoveEndpointsCascadePermissionCommand, RemoveEndpointsCascadePermissionResponse>
{
    private readonly IPermissionEndpointService _service;

    public RemoveEndpointsCascadePermissionCommandHandler(IPermissionEndpointService service)
    {
        _service = service;
    }
    
    public async Task<RemoveEndpointsCascadePermissionResponse> Handle(IReceiveContext<RemoveEndpointsCascadePermissionCommand> context, CancellationToken cancellationToken)
    {
        return await _service.RemoveEndpointsCascadePermissionAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
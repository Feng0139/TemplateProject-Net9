using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class RemovePermissionsCascadeEndpointCommandHandler : ICommandHandler<RemovePermissionsCascadeEndpointCommand, RemovePermissionsCascadeEndpointResponse>
{
    private readonly IPermissionEndpointService _service;
    
    public RemovePermissionsCascadeEndpointCommandHandler(IPermissionEndpointService service)
    {
        _service = service;
    }
    
    public async Task<RemovePermissionsCascadeEndpointResponse> Handle(IReceiveContext<RemovePermissionsCascadeEndpointCommand> context, CancellationToken cancellationToken)
    {
        return await _service.RemovePermissionsCascadeEndpointAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
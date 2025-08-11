using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class UnassignEndpointsFromPermissionCommandHandler : ICommandHandler<UnassignEndpointsFromPermissionCommand, UnassignEndpointsFromPermissionResponse>
{
    private readonly IPermissionEndpointService _service;

    public UnassignEndpointsFromPermissionCommandHandler(IPermissionEndpointService service)
    {
        _service = service;
    }
    
    public async Task<UnassignEndpointsFromPermissionResponse> Handle(IReceiveContext<UnassignEndpointsFromPermissionCommand> context, CancellationToken cancellationToken)
    {
        return await _service.UnassignEndpointsFromPermissionAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
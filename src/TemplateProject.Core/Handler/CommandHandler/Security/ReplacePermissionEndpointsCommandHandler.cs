using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class ReplacePermissionEndpointsCommandHandler : ICommandHandler<ReplacePermissionEndpointsCommand, ReplacePermissionEndpointsResponse>
{
    private readonly IPermissionEndpointService _service;

    public ReplacePermissionEndpointsCommandHandler(IPermissionEndpointService service)
    {
        _service = service;
    }
    
    public async Task<ReplacePermissionEndpointsResponse> Handle(IReceiveContext<ReplacePermissionEndpointsCommand> context, CancellationToken cancellationToken)
    {
        return await _service.ReplacePermissionEndpointsAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
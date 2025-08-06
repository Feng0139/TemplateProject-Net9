using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class RemoveRoleCascadePermissionCommandHandler : ICommandHandler<RemoveRoleCascadePermissionCommand, RemoveRoleCascadePermissionResponse>
{
    private readonly IRolePermissionService _service;

    public RemoveRoleCascadePermissionCommandHandler(IRolePermissionService service)
    {
        _service = service;
    }
    
    public async Task<RemoveRoleCascadePermissionResponse> Handle(IReceiveContext<RemoveRoleCascadePermissionCommand> context, CancellationToken cancellationToken)
    {
        return await _service.RemoveRoleCascadePermissionAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
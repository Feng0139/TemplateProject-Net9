using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class RemovePermissionCascadeRoleCommandHandler : ICommandHandler<RemovePermissionCascadeRoleCommand, RemovePermissionCascadeRoleResponse>
{
    private readonly IRolePermissionService _service;

    public RemovePermissionCascadeRoleCommandHandler(IRolePermissionService service)
    {
        _service = service;
    }
    
    public async Task<RemovePermissionCascadeRoleResponse> Handle(IReceiveContext<RemovePermissionCascadeRoleCommand> context, CancellationToken cancellationToken)
    {
        return await _service.RemovePermissionCascadeRoleAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
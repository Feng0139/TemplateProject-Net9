using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class AssignPermissionsToRoleCommandHandler : ICommandHandler<AssignPermissionsToRoleCommand, AssignPermissionsToRoleResponse>
{
    private readonly IRolePermissionService _service;

    public AssignPermissionsToRoleCommandHandler(IRolePermissionService service)
    {
        _service = service;
    }
    
    public async Task<AssignPermissionsToRoleResponse> Handle(IReceiveContext<AssignPermissionsToRoleCommand> context, CancellationToken cancellationToken)
    {
        return await _service.AssignPermissionsToRoleAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
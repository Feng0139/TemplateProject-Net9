using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class ReplaceRolePermissionsCommandHandler : ICommandHandler<ReplaceRolePermissionsCommand, ReplaceRolePermissionsResponse>
{
    private readonly IRolePermissionService _service;

    public ReplaceRolePermissionsCommandHandler(IRolePermissionService service)
    {
        _service = service;
    }

    public async Task<ReplaceRolePermissionsResponse> Handle(IReceiveContext<ReplaceRolePermissionsCommand> context, CancellationToken cancellationToken)
    {
        return await _service.ReplaceRolePermissionsAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
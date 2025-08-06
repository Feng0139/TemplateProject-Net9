using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand, UpdateRoleResponse>
{
    private readonly IRoleService _service;

    public UpdateRoleCommandHandler(IRoleService service)
    {
        _service = service;
    }

    public async Task<UpdateRoleResponse> Handle(IReceiveContext<UpdateRoleCommand> context, CancellationToken cancellationToken)
    {
        return await _service.UpdateRoleAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
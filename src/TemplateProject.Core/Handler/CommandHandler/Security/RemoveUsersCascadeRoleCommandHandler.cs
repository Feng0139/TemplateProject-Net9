using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class RemoveUsersCascadeRoleCommandHandler : ICommandHandler<RemoveUsersCascadeRoleCommand, RemoveUsersCascadeRoleResponse>
{
    private readonly IUserRoleService _service;

    public RemoveUsersCascadeRoleCommandHandler(IUserRoleService service)
    {
        _service = service;
    }
    
    public async Task<RemoveUsersCascadeRoleResponse> Handle(IReceiveContext<RemoveUsersCascadeRoleCommand> context, CancellationToken cancellationToken)
    {
        return await _service.RemoveUsersCascadeRoleAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
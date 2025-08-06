using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class RemoveRolesCascadeUserCommandHandler : ICommandHandler<RemoveRolesCascadeUserCommand, RemoveRolesCascadeUserResponse>
{
    private readonly IUserRoleService _service;

    public RemoveRolesCascadeUserCommandHandler(IUserRoleService service)
    {
        _service = service;
    }
    
    public async Task<RemoveRolesCascadeUserResponse> Handle(IReceiveContext<RemoveRolesCascadeUserCommand> context, CancellationToken cancellationToken)
    {
        return await _service.RemoveRolesCascadeUserAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
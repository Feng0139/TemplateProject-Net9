using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class AssignRolesToUserCommandHandler : ICommandHandler<AssignRolesToUserCommand, AssignRolesToUserResponse>
{
    private readonly IUserRoleService _service;

    public AssignRolesToUserCommandHandler(IUserRoleService service)
    {
        _service = service;
    }
    
    public async Task<AssignRolesToUserResponse> Handle(IReceiveContext<AssignRolesToUserCommand> context, CancellationToken cancellationToken)
    {
        return await _service.AssignRolesToUserAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
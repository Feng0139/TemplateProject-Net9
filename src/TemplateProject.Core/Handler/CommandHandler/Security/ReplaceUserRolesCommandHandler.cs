using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class ReplaceUserRolesCommandHandler : ICommandHandler<ReplaceUserRolesCommand, ReplaceUserRolesResponse>
{
    private readonly IUserRoleService _service;

    public ReplaceUserRolesCommandHandler(IUserRoleService service)
    {
        _service = service;
    }
    
    public async Task<ReplaceUserRolesResponse> Handle(IReceiveContext<ReplaceUserRolesCommand> context, CancellationToken cancellationToken)
    {
        return await _service.ReplaceUserRolesAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
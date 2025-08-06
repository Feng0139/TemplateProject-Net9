using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class AddRoleCommandHandler : ICommandHandler<AddRoleCommand, AddRoleResponse>
{
    private readonly IRoleService _service;

    public AddRoleCommandHandler(IRoleService service)
    {
        _service = service;
    }

    public async Task<AddRoleResponse> Handle(IReceiveContext<AddRoleCommand> context, CancellationToken cancellationToken)
    {
        return await _service.AddRoleAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
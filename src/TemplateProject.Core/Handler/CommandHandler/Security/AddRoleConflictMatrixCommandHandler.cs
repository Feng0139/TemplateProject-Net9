using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class AddRoleConflictMatrixCommandHandler : ICommandHandler<AddRoleConflictMatrixCommand, AddRoleConflictMatrixResponse>
{
    private readonly IRoleService _service;

    public AddRoleConflictMatrixCommandHandler(IRoleService service)
    {
        _service = service;
    }

    public async Task<AddRoleConflictMatrixResponse> Handle(IReceiveContext<AddRoleConflictMatrixCommand> context, CancellationToken cancellationToken)
    {
        return await _service.AddRoleConflictMatrixAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
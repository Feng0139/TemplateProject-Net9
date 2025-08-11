using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class DeleteRoleConflictMatrixCommandHandler : ICommandHandler<DeleteRoleConflictMatrixCommand, DeleteRoleConflictMatrixResponse>
{
    private readonly IRoleService _service;

    public DeleteRoleConflictMatrixCommandHandler(IRoleService service)
    {
        _service = service;
    }

    public async Task<DeleteRoleConflictMatrixResponse> Handle(IReceiveContext<DeleteRoleConflictMatrixCommand> context, CancellationToken cancellationToken)
    {
        return await _service.DeleteRoleConflictMatrixAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
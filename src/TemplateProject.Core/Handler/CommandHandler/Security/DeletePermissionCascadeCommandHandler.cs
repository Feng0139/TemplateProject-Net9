using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class DeletePermissionCascadeCommandHandler : ICommandHandler<DeletePermissionCascadeCommand, DeletePermissionCascadeResponse>
{
    private readonly IPermissionService _service;

    public DeletePermissionCascadeCommandHandler(IPermissionService service)
    {
        _service = service;
    }
    
    public async Task<DeletePermissionCascadeResponse> Handle(IReceiveContext<DeletePermissionCascadeCommand> context, CancellationToken cancellationToken)
    {
        return await _service.DeletePermissionCascadeAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
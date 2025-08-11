using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class UpdatePermissionCommandHandler : ICommandHandler<UpdatePermissionCommand, UpdatePermissionResponse>
{
    private readonly IPermissionService _service;

    public UpdatePermissionCommandHandler(IPermissionService service)
    {
        _service = service;
    }
    
    public async Task<UpdatePermissionResponse> Handle(IReceiveContext<UpdatePermissionCommand> context, CancellationToken cancellationToken)
    {
        return await _service.UpdatePermissionAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
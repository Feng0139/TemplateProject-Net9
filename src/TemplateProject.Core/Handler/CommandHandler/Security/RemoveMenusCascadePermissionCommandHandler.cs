using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class RemoveMenusCascadePermissionCommandHandler : ICommandHandler<RemoveMenusCascadePermissionCommand, RemoveMenusCascadePermissionResponse>
{
    private readonly IPermissionMenuService _service;
    
    public RemoveMenusCascadePermissionCommandHandler(IPermissionMenuService service)
    {
        _service = service;
    }
    
    public async Task<RemoveMenusCascadePermissionResponse> Handle(IReceiveContext<RemoveMenusCascadePermissionCommand> context, CancellationToken cancellationToken)
    {
        return await _service.RemoveMenusCascadePermissionAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
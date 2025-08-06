using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class RemovePermissionsCascadeMenuCommandHandler : ICommandHandler<RemovePermissionsCascadeMenuCommand, RemovePermissionsCascadeMenuResponse>
{
    private readonly IPermissionMenuService _service;
    
    public RemovePermissionsCascadeMenuCommandHandler(IPermissionMenuService service)
    {
        _service = service;
    }
    
    public async Task<RemovePermissionsCascadeMenuResponse> Handle(IReceiveContext<RemovePermissionsCascadeMenuCommand> context, CancellationToken cancellationToken)
    {
        return await _service.RemovePermissionsCascadeMenuAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
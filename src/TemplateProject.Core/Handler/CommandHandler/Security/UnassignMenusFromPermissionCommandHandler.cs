using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class UnassignMenusFromPermissionCommandHandler : ICommandHandler<UnassignMenusFromPermissionCommand, UnassignMenusFromPermissionResponse>
{
    private readonly IPermissionMenuService _service;

    public UnassignMenusFromPermissionCommandHandler(IPermissionMenuService service)
    {
        _service = service;
    }
    
    public async Task<UnassignMenusFromPermissionResponse> Handle(IReceiveContext<UnassignMenusFromPermissionCommand> context, CancellationToken cancellationToken)
    {
        return await _service.UnassignMenusFromPermissionAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
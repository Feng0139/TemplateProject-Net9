using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class AssignMenusToPermissionCommandHandler : ICommandHandler<AssignMenusToPermissionCommand, AssignMenusToPermissionResponse>
{
    private readonly IPermissionMenuService _service;
    
    public AssignMenusToPermissionCommandHandler(IPermissionMenuService service)
    {
        _service = service;
    }
    
    public async Task<AssignMenusToPermissionResponse> Handle(IReceiveContext<AssignMenusToPermissionCommand> context, CancellationToken cancellationToken)
    {
        return await _service.AssignMenusToPermissionAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
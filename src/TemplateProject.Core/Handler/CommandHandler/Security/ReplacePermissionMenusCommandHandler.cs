using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class ReplacePermissionMenusCommandHandler : ICommandHandler<ReplacePermissionMenusCommand, ReplacePermissionMenusResponse>
{
    private readonly IPermissionMenuService _service;
    
    public ReplacePermissionMenusCommandHandler(IPermissionMenuService service)
    {
        _service = service;
    }
    
    public async Task<ReplacePermissionMenusResponse> Handle(IReceiveContext<ReplacePermissionMenusCommand> context, CancellationToken cancellationToken)
    {
        return await _service.ReplacePermissionMenusAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
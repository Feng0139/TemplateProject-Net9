using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class AddPermissionCommandHandler : ICommandHandler<AddPermissionCommand, AddPermissionResponse>
{
    private readonly IPermissionService _service;

    public AddPermissionCommandHandler(IPermissionService service)
    {
        _service = service;
    }
    
    public async Task<AddPermissionResponse> Handle(IReceiveContext<AddPermissionCommand> context, CancellationToken cancellationToken)
    {
        return await _service.AddPermissionAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class DeleteRoleCascadeCommandHandler : ICommandHandler<DeleteRoleCascadeCommand, DeleteRoleCascadeResponse>
{
    private readonly IRoleService _service;

    public DeleteRoleCascadeCommandHandler(IRoleService service)
    {
        _service = service;
    }

    public async Task<DeleteRoleCascadeResponse> Handle(IReceiveContext<DeleteRoleCascadeCommand> context, CancellationToken cancellationToken)
    {
        return await _service.DeleteRoleCascadeAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
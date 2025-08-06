using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class DeleteMenuCascadeCommandHandler : ICommandHandler<DeleteMenuCascadeCommand, DeleteMenuCascadeResponse>
{
    private readonly IMenuService _service;

    public DeleteMenuCascadeCommandHandler(IMenuService service)
    {
        _service = service;
    }

    public async Task<DeleteMenuCascadeResponse> Handle(IReceiveContext<DeleteMenuCascadeCommand> context, CancellationToken cancellationToken)
    {
        return await _service.DeleteMenuCascadeAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
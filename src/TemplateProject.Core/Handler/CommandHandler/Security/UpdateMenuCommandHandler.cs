using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class UpdateMenuCommandHandler : ICommandHandler<UpdateMenuCommand, UpdateMenuResponse>
{
    private readonly IMenuService _service;

    public UpdateMenuCommandHandler(IMenuService service)
    {
        _service = service;
    }

    public async Task<UpdateMenuResponse> Handle(IReceiveContext<UpdateMenuCommand> context, CancellationToken cancellationToken)
    {
        return await _service.UpdateMenuAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
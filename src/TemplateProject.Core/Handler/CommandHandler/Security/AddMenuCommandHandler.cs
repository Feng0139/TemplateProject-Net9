using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Core.Handler.CommandHandler.Security;

public class AddMenuCommandHandler : ICommandHandler<AddMenuCommand, AddMenuResponse>
{
    private readonly IMenuService _service;

    public AddMenuCommandHandler(IMenuService service)
    {
        _service = service;
    }

    public async Task<AddMenuResponse> Handle(IReceiveContext<AddMenuCommand> context, CancellationToken cancellationToken)
    {
        return await _service.AddMenuAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
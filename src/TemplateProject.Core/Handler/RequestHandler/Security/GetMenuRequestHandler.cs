using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Handler.RequestHandler.Security;

public class GetMenuRequestHandler : IRequestHandler<GetMenuRequest, GetMenuResponse>
{
    private readonly IMenuService _service;

    public GetMenuRequestHandler(IMenuService service)
    {
        _service = service;
    }
    
    public async Task<GetMenuResponse> Handle(IReceiveContext<GetMenuRequest> context, CancellationToken cancellationToken)
    {
        return await _service.GetMenuAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Handler.RequestHandler.Security;

public class GetMenuTreeRequestHandler : IRequestHandler<GetMenuTreeRequest, GetMenuTreeResponse>
{
    private readonly IMenuService _service;

    public GetMenuTreeRequestHandler(IMenuService service)
    {
        _service = service;
    }
    
    public async Task<GetMenuTreeResponse> Handle(IReceiveContext<GetMenuTreeRequest> context, CancellationToken cancellationToken)
    {
        return await _service.GetMenuTreeAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
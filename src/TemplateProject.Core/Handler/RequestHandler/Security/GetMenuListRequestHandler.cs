using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Handler.RequestHandler.Security;

public class GetMenuListRequestHandler : IRequestHandler<GetMenuListRequest, GetMenuListResponse>
{
    private readonly IMenuService _service;

    public GetMenuListRequestHandler(IMenuService service)
    {
        _service = service;
    }
    
    public async Task<GetMenuListResponse> Handle(IReceiveContext<GetMenuListRequest> context, CancellationToken cancellationToken)
    {
        return await _service.GetMenuListAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
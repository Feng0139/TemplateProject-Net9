using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Handler.RequestHandler.Security;

public class GetPermissionListRequestHandler : IRequestHandler<GetPermissionListRequest, GetPermissionListResponse>
{
    private readonly IPermissionService _service;

    public GetPermissionListRequestHandler(IPermissionService service)
    {
        _service = service;
    }
    
    public async Task<GetPermissionListResponse> Handle(IReceiveContext<GetPermissionListRequest> context, CancellationToken cancellationToken)
    {
        return await _service.GetPermissionListAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
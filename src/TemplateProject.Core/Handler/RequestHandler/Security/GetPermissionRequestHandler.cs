using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Handler.RequestHandler.Security;

public class GetPermissionRequestHandler : IRequestHandler<GetPermissionRequest, GetPermissionResponse>
{
    private readonly IPermissionService _service;

    public GetPermissionRequestHandler(IPermissionService service)
    {
        _service = service;
    }
    
    public async Task<GetPermissionResponse> Handle(IReceiveContext<GetPermissionRequest> context, CancellationToken cancellationToken)
    {
        return await _service.GetPermissionAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
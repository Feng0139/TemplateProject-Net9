using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Handler.RequestHandler.Security;

public class GetEndpointsByPermissionIdsRequestHandler : IRequestHandler<GetEndpointsByPermissionIdsRequest, GetEndpointsByPermissionIdsResponse>
{
    private readonly IPermissionEndpointService _service;

    public GetEndpointsByPermissionIdsRequestHandler(IPermissionEndpointService service)
    {
        _service = service;
    }
    
    public async Task<GetEndpointsByPermissionIdsResponse> Handle(IReceiveContext<GetEndpointsByPermissionIdsRequest> context, CancellationToken cancellationToken)
    {
        return await _service.GetEndpointsByPermissionIdsAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
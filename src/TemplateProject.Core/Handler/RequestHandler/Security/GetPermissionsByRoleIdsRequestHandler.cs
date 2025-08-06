using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Handler.RequestHandler.Security;

public class GetPermissionsByRoleIdsRequestHandler : IRequestHandler<GetPermissionsByRoleIdsRequest, GetPermissionsByRoleIdsResponse>
{
    private readonly IRolePermissionService _service;

    public GetPermissionsByRoleIdsRequestHandler(IRolePermissionService service)
    {
        _service = service;
    }
    
    public async Task<GetPermissionsByRoleIdsResponse> Handle(IReceiveContext<GetPermissionsByRoleIdsRequest> context, CancellationToken cancellationToken)
    {
        return await _service.GetPermissionsByRoleIdsAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
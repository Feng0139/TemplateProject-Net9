using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Handler.RequestHandler.Security;

public class GetRoleListRequestHandler : IRequestHandler<GetRoleListRequest, GetRoleListResponse>
{
    private readonly IRoleService _service;

    public GetRoleListRequestHandler(IRoleService service)
    {
        _service = service;
    }
    
    public async Task<GetRoleListResponse> Handle(IReceiveContext<GetRoleListRequest> context, CancellationToken cancellationToken)
    {
        return await _service.GetRoleListAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Handler.RequestHandler.Security;

public class GetRoleRequestHandler : IRequestHandler<GetRoleRequest, GetRoleResponse>
{
    private readonly IRoleService _service;

    public GetRoleRequestHandler(IRoleService service)
    {
        _service = service;
    }
    
    public async Task<GetRoleResponse> Handle(IReceiveContext<GetRoleRequest> context, CancellationToken cancellationToken)
    {
        return await _service.GetRoleAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
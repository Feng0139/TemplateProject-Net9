using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Handler.RequestHandler.Security;

public class GetRoleTreeRequestHandler : IRequestHandler<GetRoleTreeRequest, GetRoleTreeResponse>
{
    private readonly IRoleService _service;

    public GetRoleTreeRequestHandler(IRoleService service)
    {
        _service = service;
    }
    
    public async Task<GetRoleTreeResponse> Handle(IReceiveContext<GetRoleTreeRequest> context, CancellationToken cancellationToken)
    {
        return await _service.GetRoleTreeAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Handler.RequestHandler.Security;

public class CheckRoleConflictExistsRequestHandler : IRequestHandler<CheckRoleConflictExistsRequest, CheckRoleConflictExistsResponse>
{
    private readonly IRoleService _service;

    public CheckRoleConflictExistsRequestHandler(IRoleService service)
    {
        _service = service;
    }
    
    public async Task<CheckRoleConflictExistsResponse> Handle(IReceiveContext<CheckRoleConflictExistsRequest> context, CancellationToken cancellationToken)
    {
        return await _service.CheckRoleConflictExistsAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
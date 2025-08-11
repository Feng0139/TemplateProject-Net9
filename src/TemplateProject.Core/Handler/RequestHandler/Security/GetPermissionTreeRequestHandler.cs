using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Handler.RequestHandler.Security;

public class GetPermissionTreeRequestHandler : IRequestHandler<GetPermissionTreeRequest, GetPermissionTreeResponse>
{
    private readonly IPermissionService _service;

    public GetPermissionTreeRequestHandler(IPermissionService service)
    {
        _service = service;
    }
    
    public async Task<GetPermissionTreeResponse> Handle(IReceiveContext<GetPermissionTreeRequest> context, CancellationToken cancellationToken)
    {
        return await _service.GetPermissionTreeAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}
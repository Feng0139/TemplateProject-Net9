using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Account;
using TemplateProject.Message.Request.Account;

namespace TemplateProject.Core.Handler.RequestHandler.Account;

public class GetThirdPartyByThirdPartyRequestHandler : IRequestHandler<GetThirdPartyByThirdPartyRequest, GetThirdPartyByThirdPartyResponse>
{
    private readonly IAccountService _service;

    public GetThirdPartyByThirdPartyRequestHandler(IAccountService service)
    {
        _service = service;
    }
    
    public async Task<GetThirdPartyByThirdPartyResponse> Handle(IReceiveContext<GetThirdPartyByThirdPartyRequest> context, CancellationToken cancellationToken)
    {
        return await _service.GetThirdPartyByThirdPartyAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}

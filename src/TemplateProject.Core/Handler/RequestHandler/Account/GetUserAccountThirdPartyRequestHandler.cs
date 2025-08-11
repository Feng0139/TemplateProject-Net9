using Mediator.Net.Context;
using Mediator.Net.Contracts;
using TemplateProject.Core.Services.Account;
using TemplateProject.Message.Request.Account;

namespace TemplateProject.Core.Handler.RequestHandler.Account;

public class GetUserAccountThirdPartyRequestHandler : IRequestHandler<GetUserAccountByThirdPartyRequest, GetUserAccountByThirdPartyResponse>
{
    private readonly IAccountService _service;

    public GetUserAccountThirdPartyRequestHandler(IAccountService service)
    {
        _service = service;
    }
    
    public async Task<GetUserAccountByThirdPartyResponse> Handle(IReceiveContext<GetUserAccountByThirdPartyRequest> context, CancellationToken cancellationToken)
    {
        return await _service.GetUserAccountByThirdPartyAsync(context.Message, cancellationToken).ConfigureAwait(false);
    }
}

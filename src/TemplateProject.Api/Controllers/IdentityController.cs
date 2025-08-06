using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateProject.Api.Filters;
using TemplateProject.Message.Commands.Account;
using TemplateProject.Message.Commands.Identity;
using TemplateProject.Message.Request.Identity;

namespace TemplateProject.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Security]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUserAccount([FromBody]RegisterUserAccountCommand command)
    {
        var response = await _mediator.SendAsync<RegisterUserAccountCommand, RegisterUserAccountResponse>(command).ConfigureAwait(false);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("login")]
    public async Task<IActionResult> LoginAsync([FromQuery]AuthenticateRequest command)
    {
        var response = await _mediator.RequestAsync<AuthenticateRequest, AuthenticateResponse>(command).ConfigureAwait(false);

        return Ok(response);
    }

    [HttpPost]
    [Route("apikey/cretae")]
    public async Task<IActionResult> CreateApiKeyAsync([FromBody]CreateApiKeyCommand request)
    {
        var response = await _mediator.SendAsync<CreateApiKeyCommand, CreateApiKeyResponse>(request).ConfigureAwait(false);

        return Ok(response);
    }
    
    [HttpPost]
    [Route("apikey/delete")]
    public async Task<IActionResult> DeleteApiKeyAsync([FromBody]DeleteApiKeyCommand request)
    {
        var response = await _mediator.SendAsync<DeleteApiKeyCommand, DeleteApiKeyResponse>(request).ConfigureAwait(false);

        return Ok(response);
    }
}
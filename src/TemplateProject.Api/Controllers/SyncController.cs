using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateProject.Api.Filters;
using TemplateProject.Message.Commands.Sync;

namespace TemplateProject.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Security]
public class SyncController : ControllerBase
{
    private readonly IMediator _mediator;

    public SyncController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("destiny2/manifest")]
    public async Task<IActionResult> SynchronizeDestiny2ManifestAsync([FromBody]SynchronizeDestiny2ManifestCommand command)
    {
        var response = await _mediator.SendAsync<SynchronizeDestiny2ManifestCommand, SynchronizeDestiny2ManifestResponse>(command).ConfigureAwait(false);

        return Ok(response);
    }
}
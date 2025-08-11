using TemplateProject.Message.Commands.Security;
using TemplateProject.Message.Dto.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Services.Security;

public interface IEndpointService : ISingleton
{
    public Task<GetEndpointListResponse> GetEndpointListAsync(GetEndpointListRequest request);
    
    public Task ReplaceEndpointListAsync(ReplaceEndpointListCommand command);
}

public class EndpointService : IEndpointService
{
    private List<EndpointDto> _endpoints = [];

    public async Task<GetEndpointListResponse> GetEndpointListAsync(GetEndpointListRequest request)
    {
        return new GetEndpointListResponse
        {
            Data = _endpoints
        };
    }

    public Task ReplaceEndpointListAsync(ReplaceEndpointListCommand command)
    {
        _endpoints = command.Endpoints;
        
        return Task.CompletedTask;
    }
}
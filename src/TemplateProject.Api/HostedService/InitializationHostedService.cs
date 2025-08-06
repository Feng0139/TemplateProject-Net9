using TemplateProject.Api.Extension;
using TemplateProject.Core.Services.Security;
using TemplateProject.Message.Commands.Security;

namespace TemplateProject.Api.HostedService;

public class InitializationHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public InitializationHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await ScanEndpointsAsync(cancellationToken).ConfigureAwait(false);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    
    private async Task ScanEndpointsAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var endpointService = scope.ServiceProvider.GetRequiredService<IEndpointService>();

        var endpoints = ScannerExtensions.ScanEndpoints();

        await endpointService.ReplaceEndpointListAsync(new ReplaceEndpointListCommand
        {
            Endpoints = endpoints
        });
    }
}
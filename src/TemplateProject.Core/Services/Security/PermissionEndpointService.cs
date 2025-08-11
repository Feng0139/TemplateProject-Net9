using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TemplateProject.Core.Data;
using TemplateProject.Core.Domain.Security;
using TemplateProject.Message.Commands.Security;
using TemplateProject.Message.Dto.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Services.Security;

public interface IPermissionEndpointService : IScope
{
    Task<GetEndpointsByPermissionIdsResponse> GetEndpointsByPermissionIdsAsync(
        GetEndpointsByPermissionIdsRequest request, CancellationToken cancellationToken);
    
    Task<AssignEndpointsToPermissionResponse> AssignEndpointsToPermissionAsync(
        AssignEndpointsToPermissionCommand command, CancellationToken cancellationToken);
    
    Task<UnassignEndpointsFromPermissionResponse> UnassignEndpointsFromPermissionAsync(
        UnassignEndpointsFromPermissionCommand command, CancellationToken cancellationToken);
    
    Task<ReplacePermissionEndpointsResponse> ReplacePermissionEndpointsAsync(
        ReplacePermissionEndpointsCommand command, CancellationToken cancellationToken);
    
    Task<RemovePermissionsCascadeEndpointResponse> RemovePermissionsCascadeEndpointAsync(
        RemovePermissionsCascadeEndpointCommand command, CancellationToken cancellationToken);
    
    Task<RemoveEndpointsCascadePermissionResponse> RemoveEndpointsCascadePermissionAsync(
        RemoveEndpointsCascadePermissionCommand command, CancellationToken cancellationToken);
}

public class PermissionEndpointService : IPermissionEndpointService
{
    private readonly IMapper _mapper;
    private readonly IPermissionDataProvider _permissionDataProvider;
    private readonly IEndpointService _endpointService;
    private readonly IRepository<PermissionEndpoint> _permissionEndpointRep;
    
    public PermissionEndpointService(
        IMapper mapper,
        IPermissionDataProvider permissionDataProvider,
        IEndpointService endpointService,
        IRepository<PermissionEndpoint> permissionEndpointRep)
    {
        _mapper = mapper;
        _permissionDataProvider = permissionDataProvider;
        _endpointService = endpointService;
        _permissionEndpointRep = permissionEndpointRep;
    }

    public async Task<GetEndpointsByPermissionIdsResponse> GetEndpointsByPermissionIdsAsync(
        GetEndpointsByPermissionIdsRequest request, CancellationToken cancellationToken)
    {
        var response = new GetEndpointsByPermissionIdsResponse();
        
        var endpoints = await _permissionEndpointRep.QueryNoTracking()
            .Where(x => request.PermissionIds.Contains(x.Id))
            .Select(x => x.Endpoint)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
        
        response.Data = await GetExistEndpointDtosAsync(endpoints).ConfigureAwait(false);
        return response;
    }

    public async Task<AssignEndpointsToPermissionResponse> AssignEndpointsToPermissionAsync(
        AssignEndpointsToPermissionCommand command, CancellationToken cancellationToken)
    {
        var response = new AssignEndpointsToPermissionResponse();
        
        var permission = await _permissionDataProvider.GetPermissionAsync(command.PermissionId, cancellationToken).ConfigureAwait(false);
        if (permission == null)
        {
            response.Code = HttpStatusCode.BadRequest;
            response.Message = "Permission not found.";
            return response;
        }
        
        var endpoints = command.Endpoints.Distinct().ToList();
        var existingEndpointDtos = await GetExistEndpointDtosAsync(endpoints).ConfigureAwait(false);
        var nonExistingEndpoints = endpoints.Where(e => existingEndpointDtos.All(ed => ed.Endpoint != e)).ToList();

        if (nonExistingEndpoints.Count != 0)
        {
            response.Code = HttpStatusCode.BadRequest;
            response.Message = $"Endpoints not found: {string.Join(", ", nonExistingEndpoints)}";
            return response;
        }
        
        var existingPermissionEndpoints = await _permissionEndpointRep.QueryNoTracking()
            .Where(x => x.PermissionId == command.PermissionId && endpoints.Contains(x.Endpoint))
            .Select(x => x.Endpoint)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
        
        var newEndpoints = endpoints.Except(existingPermissionEndpoints).ToList();
        
        var newPermissionEndpoints = newEndpoints.Select(e => new PermissionEndpoint
        {
            PermissionId = command.PermissionId,
            Endpoint = e
        }).ToList();
        
        await _permissionEndpointRep.InsertManyAsync(newPermissionEndpoints, cancellationToken).ConfigureAwait(false);
        
        return response;
    }

    public async Task<UnassignEndpointsFromPermissionResponse> UnassignEndpointsFromPermissionAsync(
        UnassignEndpointsFromPermissionCommand command, CancellationToken cancellationToken)
    {
        var response = new UnassignEndpointsFromPermissionResponse();

        await _permissionEndpointRep.DeleteAsync(x => x.PermissionId == command.PermissionId && command.Endpoints.Contains(x.Endpoint), cancellationToken).ConfigureAwait(false);

        return response;
    }

    public async Task<ReplacePermissionEndpointsResponse> ReplacePermissionEndpointsAsync(
        ReplacePermissionEndpointsCommand command, CancellationToken cancellationToken)
    {
        var response = new ReplacePermissionEndpointsResponse();
        
        var permission = await _permissionDataProvider.GetPermissionAsync(command.PermissionId, cancellationToken).ConfigureAwait(false);
        if (permission == null)
        {
            response.Code = HttpStatusCode.BadRequest;
            response.Message = "Permission not found.";
            return response;
        }
        
        await _permissionEndpointRep.DeleteAsync(x => x.PermissionId == command.PermissionId ,cancellationToken).ConfigureAwait(false);

        if (command.Endpoints.Count == 0)
        {
            return response;
        }
        
        var endpoints = command.Endpoints.Distinct().ToList();
        var existingEndpointDtos = await GetExistEndpointDtosAsync(endpoints).ConfigureAwait(false);
        
        var newPermissionEndpoints = existingEndpointDtos.Select(e => new PermissionEndpoint
        {
            PermissionId = command.PermissionId,
            Endpoint = e.Endpoint
        }).ToList();
        
        await _permissionEndpointRep.InsertManyAsync(newPermissionEndpoints, cancellationToken).ConfigureAwait(false);
        
        return response;
    }

    public async Task<RemovePermissionsCascadeEndpointResponse> RemovePermissionsCascadeEndpointAsync(
        RemovePermissionsCascadeEndpointCommand command, CancellationToken cancellationToken)
    {
        var response = new RemovePermissionsCascadeEndpointResponse();
        
        await _permissionEndpointRep.DeleteAsync(x => command.PermissionIds.Contains(x.PermissionId), cancellationToken).ConfigureAwait(false);
        
        return response;
    }

    public async Task<RemoveEndpointsCascadePermissionResponse> RemoveEndpointsCascadePermissionAsync(
        RemoveEndpointsCascadePermissionCommand command, CancellationToken cancellationToken)
    {
        var response = new RemoveEndpointsCascadePermissionResponse();
        
        await _permissionEndpointRep.DeleteAsync(x => command.Endpoints.Contains(x.Endpoint), cancellationToken).ConfigureAwait(false);
        
        return response;
    }

    private async Task<List<EndpointDto>> GetExistEndpointDtosAsync(List<string> endpoints)
    {
        var result = await _endpointService.GetEndpointListAsync(new GetEndpointListRequest()).ConfigureAwait(false);

        return result.Data != null 
            ? result.Data.Where(x => endpoints.Contains(x.Endpoint)).ToList()
            : [];
    }
}
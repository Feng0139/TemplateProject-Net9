using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TemplateProject.Core.Data;
using TemplateProject.Core.Domain.Security;
using TemplateProject.Message.Commands.Security;
using TemplateProject.Message.Dto.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Services.Security;

public interface IRolePermissionService : IScope
{
    Task<GetPermissionsByRoleIdsResponse> GetPermissionsByRoleIdsAsync(
        GetPermissionsByRoleIdsRequest request, CancellationToken cancellationToken);
    
    Task<GetRoleHasPermissionResponse> GetRoleHasPermissionAsync(
        GetRoleHasPermissionRequest request, CancellationToken cancellationToken);
    
    Task<AssignPermissionsToRoleResponse> AssignPermissionsToRoleAsync(
        AssignPermissionsToRoleCommand command, CancellationToken cancellationToken);
    
    Task<UnassignPermissionsFromRoleResponse> UnassignPermissionsFromRoleAsync(
        UnassignPermissionsFromRoleCommand command, CancellationToken cancellationToken);
    
    Task<ReplaceRolePermissionsResponse> ReplaceRolePermissionsAsync(
        ReplaceRolePermissionsCommand command, CancellationToken cancellationToken);
    
    Task<RemoveRoleCascadePermissionResponse> RemoveRoleCascadePermissionAsync(
        RemoveRoleCascadePermissionCommand command, CancellationToken cancellationToken);
    
    Task<RemovePermissionCascadeRoleResponse> RemovePermissionCascadeRoleAsync(
        RemovePermissionCascadeRoleCommand command, CancellationToken cancellationToken);
}

public class RolePermissionService : IRolePermissionService
{
    private readonly IMapper _mapper;
    private readonly IRoleDataProvider _roleDataProvider;
    private readonly IPermissionDataProvider _permissionDataProvider;
    private readonly IRepository<RolePermission> _rolePermissionRep;

    public RolePermissionService(
        IMapper mapper,
        IRoleDataProvider roleDataProvider,
        IPermissionDataProvider permissionDataProvider,
        IRepository<RolePermission> rolePermissionRep)
    {
        _mapper = mapper;
        _roleDataProvider = roleDataProvider;
        _permissionDataProvider = permissionDataProvider;
        _rolePermissionRep = rolePermissionRep;
    }

    public async Task<GetPermissionsByRoleIdsResponse> GetPermissionsByRoleIdsAsync(
        GetPermissionsByRoleIdsRequest request, CancellationToken cancellationToken)
    {
        var list = await _rolePermissionRep.QueryNoTracking()
            .Where(x => request.RoleIds.Contains(x.Id))
            .Select(x => x.PermissionId)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
        
        var permissions = await _permissionDataProvider.GetPermissionsAsync(list, cancellationToken).ConfigureAwait(false);

        return new GetPermissionsByRoleIdsResponse
        {
            Data = _mapper.Map<List<PermissionDto>>(permissions)
        };
    }

    public async Task<GetRoleHasPermissionResponse> GetRoleHasPermissionAsync(
        GetRoleHasPermissionRequest request, CancellationToken cancellationToken)
    {
        var has = await _rolePermissionRep.QueryNoTracking()
            .AnyAsync(x => x.RoleId == request.RoleId && x.PermissionId == request.PermissionId, cancellationToken).ConfigureAwait(false);
        
        return new GetRoleHasPermissionResponse
        {
            Data = has
        };
    }

    public async Task<AssignPermissionsToRoleResponse> AssignPermissionsToRoleAsync(
        AssignPermissionsToRoleCommand command, CancellationToken cancellationToken)
    {
        var response = new AssignPermissionsToRoleResponse();
        
        var role = await _roleDataProvider.GetRoleAsync(command.RoleId, cancellationToken).ConfigureAwait(false);
        if (role == null)
        {
            response.Code = HttpStatusCode.BadRequest;
            response.Message = $"Role with ID {command.RoleId} does not exist.";
            return response;
        }
        
        var permissionIds = command.PermissionIds.Distinct().ToList();
        var existingPermissions = await _permissionDataProvider.GetPermissionsAsync(permissionIds, cancellationToken).ConfigureAwait(false);
        var existingPermissionIds = existingPermissions.Select(p => p.Id).ToHashSet();
        var nonExistentPermissionIds = permissionIds.Where(id => !existingPermissionIds.Contains(id)).ToList();
    
        if (nonExistentPermissionIds.Any())
        {
            response.Code = HttpStatusCode.BadRequest;
            response.Message = $"Permissions with IDs {string.Join(", ", nonExistentPermissionIds)} do not exist.";
            return response;
        }
        
        var existsIds = await _rolePermissionRep.QueryNoTracking()
            .Where(x => x.RoleId == command.RoleId && permissionIds.Contains(x.PermissionId))
            .Select(x => x.PermissionId)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
        
        var toAdd = permissionIds.Except(existsIds).ToList();
        if (toAdd.Count == 0)
        {
            response.Code = HttpStatusCode.BadRequest;
            response.Message = "All permissions already assigned to the role.";
            return response;
        }
        
        var entities = toAdd.Select(pid => new RolePermission
        {
            RoleId = command.RoleId,
            PermissionId = pid
        }).ToList();
        await _rolePermissionRep.InsertManyAsync(entities, cancellationToken).ConfigureAwait(false);
        
        return response;
    }

    public async Task<UnassignPermissionsFromRoleResponse> UnassignPermissionsFromRoleAsync(
        UnassignPermissionsFromRoleCommand command, CancellationToken cancellationToken)
    {
        var response = new UnassignPermissionsFromRoleResponse();
        
        await _rolePermissionRep.DeleteAsync(x => x.RoleId == command.RoleId && command.PermissionIds.Contains(x.PermissionId), cancellationToken).ConfigureAwait(false);
        
        return response;
    }

    public async Task<ReplaceRolePermissionsResponse> ReplaceRolePermissionsAsync(
        ReplaceRolePermissionsCommand command, CancellationToken cancellationToken)
    {
        var response = new ReplaceRolePermissionsResponse();
        
        var role = await _roleDataProvider.GetRoleAsync(command.RoleId, cancellationToken).ConfigureAwait(false);
        if (role == null)
        {
            response.Code = HttpStatusCode.BadRequest;
            response.Message = $"Role with ID {command.RoleId} does not exist.";
            return response;
        }
        
        await _rolePermissionRep.DeleteAsync(x => x.RoleId == command.RoleId, cancellationToken).ConfigureAwait(false);
        if (command.PermissionIds.Count == 0)
        {
            return response;
        }
        
        var permissionIds = command.PermissionIds.Distinct().ToList();
        var existingPermissions = await _permissionDataProvider.GetPermissionsAsync(permissionIds, cancellationToken).ConfigureAwait(false);
        
        var entities = existingPermissions.Select(p => new RolePermission
        {
            RoleId = command.RoleId,
            PermissionId = p.Id
        }).ToList();
        
        await _rolePermissionRep.InsertManyAsync(entities, cancellationToken).ConfigureAwait(false);
        
        return response;
    }

    public async Task<RemoveRoleCascadePermissionResponse> RemoveRoleCascadePermissionAsync(
        RemoveRoleCascadePermissionCommand command, CancellationToken cancellationToken)
    {
        var response = new RemoveRoleCascadePermissionResponse();
        
        await _rolePermissionRep.DeleteAsync(x => command.RoleIds.Contains(x.RoleId), cancellationToken).ConfigureAwait(false);
        
        return response;
    }

    public async Task<RemovePermissionCascadeRoleResponse> RemovePermissionCascadeRoleAsync(
        RemovePermissionCascadeRoleCommand command, CancellationToken cancellationToken)
    {
        var response = new RemovePermissionCascadeRoleResponse();
        
        await _rolePermissionRep.DeleteAsync(x => command.PermissionIds.Contains(x.PermissionId), cancellationToken).ConfigureAwait(false);
        
        return response;
    }
}
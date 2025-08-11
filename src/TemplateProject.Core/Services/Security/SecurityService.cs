using Serilog;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Services.Security;

public interface ISecurityService : IScope
{
    Task<bool> VerifyPermissionEndpointAsync(
        Guid userId, string endpoint, string method, CancellationToken cancellationToken);
}

public class SecurityService : ISecurityService
{
    private readonly IUserRoleService _userRoleService;
    private readonly IRolePermissionService _rolePermissionService;
    private readonly IPermissionEndpointService _permissionEndpointService;
    
    public SecurityService(
        IUserRoleService userRoleService,
        IRolePermissionService rolePermissionService,
        IPermissionEndpointService permissionEndpointService)
    {
        _userRoleService = userRoleService;
        _rolePermissionService = rolePermissionService;
        _permissionEndpointService = permissionEndpointService;
    }
    
    public async Task<bool> VerifyPermissionEndpointAsync(
        Guid userId, string endpoint, string method, CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty || string.IsNullOrEmpty(endpoint))
        {
            return false;
        }

        var userRoles = await _userRoleService.GetRolesByUserIdAsync(
            new GetRolesByUserIdRequest { UserId = userId }, cancellationToken).ConfigureAwait(false);
        if (userRoles.Data == null || userRoles.Data.UserRoles.Count == 0)
        {
            Log.Warning("User with ID {UserId} has no roles assigned.", userId);
            return false;
        }
        
        var roleIds = userRoles.Data.UserRoles.Where(x=> x.Deadline > DateTimeOffset.Now).Select(r => r.RoleId).ToList();
        var rolePermissions = await _rolePermissionService.GetPermissionsByRoleIdsAsync(
            new GetPermissionsByRoleIdsRequest { RoleIds = roleIds }, cancellationToken).ConfigureAwait(false);
        if (rolePermissions.Data == null || rolePermissions.Data.Count == 0)
        {
            Log.Warning("User with ID {UserId} has no permissions assigned to their roles.", userId);
            return false;
        }

        var permissionIds = rolePermissions.Data.Select(p => p.Id).ToList();
        var permissionEndpoints = await _permissionEndpointService.GetEndpointsByPermissionIdsAsync(
            new GetEndpointsByPermissionIdsRequest { PermissionIds = permissionIds }, cancellationToken).ConfigureAwait(false);
        if (permissionEndpoints.Data == null || permissionEndpoints.Data.Count == 0)
        {
            Log.Warning("User with ID {UserId} has no endpoints assigned to their permissions.", userId);
            return false;
        }
        
        var isAllowed = permissionEndpoints.Data.Any(ep => 
            string.Equals(ep.Endpoint, endpoint, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(ep.Method, method, StringComparison.OrdinalIgnoreCase));

        return isAllowed;
    }
}
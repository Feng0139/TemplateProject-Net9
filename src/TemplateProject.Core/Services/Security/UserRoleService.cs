using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TemplateProject.Core.Data;
using TemplateProject.Core.Domain.Security;
using TemplateProject.Core.Services.Account;
using TemplateProject.Message.Commands.Security;
using TemplateProject.Message.Dto.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Services.Security;

public interface IUserRoleService : IScope
{
    Task<GetRolesByUserIdResponse> GetRolesByUserIdAsync(
        GetRolesByUserIdRequest request, CancellationToken cancellationToken);
    
    Task<AssignRolesToUserResponse> AssignRolesToUserAsync(
        AssignRolesToUserCommand command, CancellationToken cancellationToken);
    
    Task<UnassignRolesFromUserResponse> UnassignRolesFromUserAsync(
        UnassignRolesFromUserCommand command, CancellationToken cancellationToken);
    
    Task<ReplaceUserRolesResponse> ReplaceUserRolesAsync(
        ReplaceUserRolesCommand command, CancellationToken cancellationToken);

    Task<RemoveRolesCascadeUserResponse> RemoveRolesCascadeUserAsync(
        RemoveRolesCascadeUserCommand command, CancellationToken cancellationToken);
    
    Task<RemoveUsersCascadeRoleResponse> RemoveUsersCascadeRoleAsync(
        RemoveUsersCascadeRoleCommand command, CancellationToken cancellationToken);
}

public class UserRoleService : IUserRoleService
{
    private readonly IMapper _mapper;
    private readonly IRoleDataProvider _roleDataProvider;
    private readonly IAccountDataProvider _accountDataProvider;
    private readonly IRepository<UserRole> _userRoleRep;
    
    public UserRoleService(
        IMapper mapper,
        IRoleDataProvider roleDataProvider,
        IAccountDataProvider accountDataProvider,
        IRepository<UserRole> userRoleRep)
    {
        _mapper = mapper;
        _roleDataProvider = roleDataProvider;
        _accountDataProvider = accountDataProvider;
        _userRoleRep = userRoleRep;
    }
    
    public async Task<GetRolesByUserIdResponse> GetRolesByUserIdAsync(
        GetRolesByUserIdRequest request, CancellationToken cancellationToken)
    {
        var response = new GetRolesByUserIdResponse
        {
            Data = new GetRolesByUserIdData()
        };
        
        var userRoles = await _userRoleRep.QueryNoTracking()
            .Where(x => x.UserId == request.UserId)
            .ToListAsync(cancellationToken).ConfigureAwait(false);

        if (userRoles.Count == 0)
        {
            return response;
        }
        
        var roleIds = userRoles.Select(x => x.RoleId).Distinct().ToList();
        var roles = await _roleDataProvider.GetRoleListAsync(roleIds, cancellationToken).ConfigureAwait(false);
        
        response.Data.UserRoles = _mapper.Map<List<UserRoleDto>>(userRoles);
        response.Data.Roles = _mapper.Map<List<RoleDto>>(roles);
        
        return response;
    }

    public async Task<AssignRolesToUserResponse> AssignRolesToUserAsync(
        AssignRolesToUserCommand command, CancellationToken cancellationToken)
    {
        var response = new AssignRolesToUserResponse();
        
        var user = await _accountDataProvider.GetUserAccountAsync(command.UserId, string.Empty, cancellationToken).ConfigureAwait(false);
        if (user == null)
        {
            response.Code = HttpStatusCode.BadRequest;
            response.Message = $"User with ID {command.UserId} does not exist.";
            return response;
        }
        
        var roleIds = command.RoleIds.Distinct().ToList();
        
        var existingRoles = await _roleDataProvider.GetRoleListAsync(roleIds, cancellationToken).ConfigureAwait(false);
        var existingRoleIds = existingRoles.Select(r => r.Id).ToList();
        var nonExistingRoleIds = roleIds.Except(existingRoleIds).ToList();

        if (nonExistingRoleIds.Count != 0)
        {
            response.Code = HttpStatusCode.BadRequest;
            response.Message = $"Role IDs do not exist: {string.Join(", ", nonExistingRoleIds)}";
            return response;
        }

        var existingRoleIdsForUser = await _userRoleRep.QueryNoTracking()
            .Where(x => x.UserId == command.UserId && roleIds.Contains(x.RoleId))
            .Select(x => x.RoleId)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
        
        var newRoleIds = roleIds.Except(existingRoleIdsForUser).ToList();

        if (newRoleIds.Count == 0)
        {
            return response;
        }
        
        var newUserRoles = newRoleIds.Select(roleId => new UserRole
        {
            UserId = command.UserId,
            RoleId = roleId
        }).ToList();
        
        await _userRoleRep.InsertManyAsync(newUserRoles, cancellationToken).ConfigureAwait(false);
        
        return response;
    }

    public async Task<UnassignRolesFromUserResponse> UnassignRolesFromUserAsync(
        UnassignRolesFromUserCommand command, CancellationToken cancellationToken)
    {
        var response = new UnassignRolesFromUserResponse();
        
        await _userRoleRep.DeleteAsync(x => x.UserId == command.UserId && command.RoleIds.Contains(x.RoleId), cancellationToken).ConfigureAwait(false);
        
        return response;
    }
    
    public async Task<ReplaceUserRolesResponse> ReplaceUserRolesAsync(
        ReplaceUserRolesCommand command, CancellationToken cancellationToken)
    {
        var response = new ReplaceUserRolesResponse();
        
        var user = await _accountDataProvider.GetUserAccountAsync(command.UserId, string.Empty, cancellationToken).ConfigureAwait(false);
        if (user == null)
        {
            response.Code = HttpStatusCode.BadRequest;
            response.Message = $"User with ID {command.UserId} does not exist.";
            return response;
        }
        
        await _userRoleRep.DeleteAsync(x => x.UserId == command.UserId, cancellationToken).ConfigureAwait(false);

        if (command.RoleIds.Count == 0)
        {
            return response;
        }
        
        var roleIds = command.RoleIds.Distinct().ToList();
        var existingRoles = await _roleDataProvider.GetRoleListAsync(roleIds, cancellationToken).ConfigureAwait(false);
        
        var newUserRoles = existingRoles.Select(role => new UserRole
        {
            UserId = command.UserId,
            RoleId = role.Id
        }).ToList();
        
        await _userRoleRep.InsertManyAsync(newUserRoles, cancellationToken).ConfigureAwait(false);
        
        return response;
    }

    public async Task<RemoveRolesCascadeUserResponse> RemoveRolesCascadeUserAsync(
        RemoveRolesCascadeUserCommand command, CancellationToken cancellationToken)
    {
        var response = new RemoveRolesCascadeUserResponse();
        
        await _userRoleRep.DeleteAsync(x => command.RoleIds.Contains(x.RoleId), cancellationToken).ConfigureAwait(false);

        return response;
    }

    public async Task<RemoveUsersCascadeRoleResponse> RemoveUsersCascadeRoleAsync(
        RemoveUsersCascadeRoleCommand command, CancellationToken cancellationToken)
    {
        var response = new RemoveUsersCascadeRoleResponse();
        
        await _userRoleRep.DeleteAsync(x => command.UserIds.Contains(x.UserId), cancellationToken).ConfigureAwait(false);
        
        return response;
    }
}
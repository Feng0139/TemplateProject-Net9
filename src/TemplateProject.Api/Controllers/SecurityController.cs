using Mediator.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateProject.Api.Filters;
using TemplateProject.Message.Attributes;
using TemplateProject.Message.Commands.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Security]
public class SecurityController : ControllerBase
{
    private readonly IMediator _mediator;

    public SecurityController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    #region Role
    
    [HttpGet("role")]
    public async Task<IActionResult> GetRoleAsync([FromQuery] GetRoleRequest request)
    {
        var response = await _mediator.RequestAsync<GetRoleRequest, GetRoleResponse>(request).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpGet("role/list")]
    public async Task<IActionResult> GetRoleListAsync([FromQuery] GetRoleListRequest request)
    {
        var response = await _mediator.RequestAsync<GetRoleListRequest, GetRoleListResponse>(request).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpGet("role/tree")]
    public async Task<IActionResult> GetRoleTreeAsync([FromQuery] GetRoleTreeRequest request)
    {
        var response = await _mediator.RequestAsync<GetRoleTreeRequest, GetRoleTreeResponse>(request).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("role/add")]
    public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleCommand command)
    {
        var response = await _mediator.SendAsync<AddRoleCommand, AddRoleResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("role/update")]
    public async Task<IActionResult> UpdateRoleAsync([FromBody] UpdateRoleCommand command)
    {
        var response = await _mediator.SendAsync<UpdateRoleCommand, UpdateRoleResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("role/delete")]
    public async Task<IActionResult> DeleteRoleAsync([FromBody] DeleteRoleCascadeCommand command)
    {
        var response = await _mediator.SendAsync<DeleteRoleCascadeCommand, DeleteRoleCascadeResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }

    #endregion
    
    #region Permission
    
    [HttpGet("permission")]
    public async Task<IActionResult> GetPermissionAsync([FromQuery] GetPermissionRequest request)
    {
        var response = await _mediator.RequestAsync<GetPermissionRequest, GetPermissionResponse>(request).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpGet("permission/list")]
    public async Task<IActionResult> GetPermissionListAsync([FromQuery] GetPermissionListRequest request)
    {
        var response = await _mediator.RequestAsync<GetPermissionListRequest, GetPermissionListResponse>(request).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpGet("permission/tree")]
    public async Task<IActionResult> GetPermissionTreeAsync([FromQuery] GetPermissionTreeRequest request)
    {
        var response = await _mediator.RequestAsync<GetPermissionTreeRequest, GetPermissionTreeResponse>(request).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("permission/add")]
    public async Task<IActionResult> AddPermissionAsync([FromBody] AddPermissionCommand command)
    {
        var response = await _mediator.SendAsync<AddPermissionCommand, AddPermissionResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("permission/update")]
    public async Task<IActionResult> UpdatePermissionAsync([FromBody] UpdatePermissionCommand command)
    {
        var response = await _mediator.SendAsync<UpdatePermissionCommand, UpdatePermissionResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("permission/delete")]
    public async Task<IActionResult> DeletePermissionAsync([FromBody] DeletePermissionCascadeCommand command)
    {
        var response = await _mediator.SendAsync<DeletePermissionCascadeCommand, DeletePermissionCascadeResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    #endregion
    
    #region Menu

    [HttpGet("menu")]
    public async Task<IActionResult> GetMenuAsync([FromQuery] GetMenuRequest request)
    {
        var response = await _mediator.RequestAsync<GetMenuRequest, GetMenuResponse>(request).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpGet("menu/list")]
    public async Task<IActionResult> GetMenuListAsync([FromQuery] GetMenuListRequest request)
    {
        var response = await _mediator.RequestAsync<GetMenuListRequest, GetMenuListResponse>(request).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpGet("menu/tree")]
    public async Task<IActionResult> GetMenuTreeAsync([FromQuery] GetMenuTreeRequest request)
    {
        var response = await _mediator.RequestAsync<GetMenuTreeRequest, GetMenuTreeResponse>(request).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("menu/add")]
    public async Task<IActionResult> AddMenuAsync([FromBody] AddMenuCommand command)
    {
        var response = await _mediator.SendAsync<AddMenuCommand, AddMenuResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("menu/update")]
    public async Task<IActionResult> UpdateMenuAsync([FromBody] UpdateMenuCommand command)
    {
        var response = await _mediator.SendAsync<UpdateMenuCommand, UpdateMenuResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("menu/delete")]
    public async Task<IActionResult> DeleteMenuAsync([FromBody] DeleteMenuCascadeCommand command)
    {
        var response = await _mediator.SendAsync<DeleteMenuCascadeCommand, DeleteMenuCascadeResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    #endregion
    
    #region Endpoint
    
    [SkipScan]
    [HttpPost("endpoint/list")]
    public async Task<IActionResult> GetEndpointListAsync([FromQuery] GetEndpointListRequest request)
    {
        var response = await _mediator.RequestAsync<GetEndpointListRequest, GetEndpointListResponse>(request).ConfigureAwait(false);
        return Ok(response);
    }
    
    #endregion
    
    #region UserRoles
    
    [HttpGet("user/roles")]
    public async Task<IActionResult> GetRolesByUserIdAsync([FromQuery] GetRolesByUserIdRequest request)
    {
        var response = await _mediator.RequestAsync<GetRolesByUserIdRequest, GetRolesByUserIdResponse>(request).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("user/assign/roles")]
    public async Task<IActionResult> AssignRolesToUserAsync([FromBody] AssignRolesToUserCommand command)
    {
        var response = await _mediator.SendAsync<AssignRolesToUserCommand, AssignRolesToUserResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("user/unassign/roles")]
    public async Task<IActionResult> UnassignRolesFromUserAsync([FromBody] UnassignRolesFromUserCommand command)
    {
        var response = await _mediator.SendAsync<UnassignRolesFromUserCommand, UnassignRolesFromUserResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("user/replace/roles")]
    public async Task<IActionResult> ReplaceUserRolesAsync([FromBody] ReplaceUserRolesCommand command)
    {
        var response = await _mediator.SendAsync<ReplaceUserRolesCommand, ReplaceUserRolesResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }

    #endregion
    
    #region RolePermissions
    
    [HttpGet("role/permissions")]
    public async Task<IActionResult> GetPermissionsByRoleIdsAsync([FromQuery] GetPermissionsByRoleIdsRequest request)
    {
        var response = await _mediator.RequestAsync<GetPermissionsByRoleIdsRequest, GetPermissionsByRoleIdsResponse>(request).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("role/assign/permissions")]
    public async Task<IActionResult> AssignPermissionsToRoleAsync([FromBody] AssignPermissionsToRoleCommand command)
    {
        var response = await _mediator.SendAsync<AssignPermissionsToRoleCommand, AssignPermissionsToRoleResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("role/unassign/permissions")]
    public async Task<IActionResult> UnassignPermissionsFromRoleAsync([FromBody] UnassignPermissionsFromRoleCommand command)
    {
        var response = await _mediator.SendAsync<UnassignPermissionsFromRoleCommand, UnassignPermissionsFromRoleResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("role/replace/permissions")]
    public async Task<IActionResult> ReplaceRolePermissionsAsync([FromBody] ReplaceRolePermissionsCommand command)
    {
        var response = await _mediator.SendAsync<ReplaceRolePermissionsCommand, ReplaceRolePermissionsResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    #endregion
    
    #region PermissionMenus
    
    [HttpGet("permission/menus")]
    public async Task<IActionResult> GetMenusByPermissionIdAsync([FromQuery] GetMenusByPermissionIdRequest request)
    {
        var response = await _mediator.RequestAsync<GetMenusByPermissionIdRequest, GetMenusByPermissionIdResponse>(request).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("permission/assign/menus")]
    public async Task<IActionResult> AssignMenusToPermissionAsync([FromBody] AssignMenusToPermissionCommand command)
    {
        var response = await _mediator.SendAsync<AssignMenusToPermissionCommand, AssignMenusToPermissionResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("permission/unassign/menus")]
    public async Task<IActionResult> UnassignMenusFromPermissionAsync([FromBody] UnassignMenusFromPermissionCommand command)
    {
        var response = await _mediator.SendAsync<UnassignMenusFromPermissionCommand, UnassignMenusFromPermissionResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("permission/replace/menus")]
    public async Task<IActionResult> ReplacePermissionMenusAsync([FromBody] ReplacePermissionMenusCommand command)
    {
        var response = await _mediator.SendAsync<ReplacePermissionMenusCommand, ReplacePermissionMenusResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    #endregion
    
    #region PermissionEndpoints
    
    [HttpGet("permission/endpoints")]
    public async Task<IActionResult> GetEndpointsByPermissionIdsAsync([FromQuery] GetEndpointsByPermissionIdsRequest request)
    {
        var response = await _mediator.RequestAsync<GetEndpointsByPermissionIdsRequest, GetEndpointsByPermissionIdsResponse>(request).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("permission/assign/endpoints")]
    public async Task<IActionResult> AssignEndpointsToPermissionAsync([FromBody] AssignEndpointsToPermissionCommand command)
    {
        var response = await _mediator.SendAsync<AssignEndpointsToPermissionCommand, AssignEndpointsToPermissionResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("permission/unassign/endpoints")]
    public async Task<IActionResult> UnassignEndpointsFromPermissionAsync([FromBody] UnassignEndpointsFromPermissionCommand command)
    {
        var response = await _mediator.SendAsync<UnassignEndpointsFromPermissionCommand, UnassignEndpointsFromPermissionResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    [HttpPost("permission/replace/endpoints")]
    public async Task<IActionResult> ReplacePermissionEndpointsAsync([FromBody] ReplacePermissionEndpointsCommand command)
    {
        var response = await _mediator.SendAsync<ReplacePermissionEndpointsCommand, ReplacePermissionEndpointsResponse>(command).ConfigureAwait(false);
        return Ok(response);
    }
    
    #endregion
}
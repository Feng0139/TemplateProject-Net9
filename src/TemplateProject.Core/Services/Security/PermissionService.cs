using System.Net;
using AutoMapper;
using TemplateProject.Core.Domain.Security;
using TemplateProject.Core.Extension;
using TemplateProject.Message.Commands.Security;
using TemplateProject.Message.Dto.Security;
using TemplateProject.Message.Enum.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Services.Security;

public partial interface IPermissionService : IScope
{
    Task<GetPermissionResponse> GetPermissionAsync(
        GetPermissionRequest request, CancellationToken cancellationToken);
    
    Task<GetPermissionListResponse> GetPermissionListAsync(
        GetPermissionListRequest request, CancellationToken cancellationToken);
    
    Task<GetPermissionTreeResponse> GetPermissionTreeAsync(
        GetPermissionTreeRequest request, CancellationToken cancellationToken);
    
    Task<AddPermissionResponse> AddPermissionAsync(
        AddPermissionCommand command, CancellationToken cancellationToken);
    
    Task<UpdatePermissionResponse> UpdatePermissionAsync(
        UpdatePermissionCommand command, CancellationToken cancellationToken);
    
    Task<DeletePermissionCascadeResponse> DeletePermissionCascadeAsync(
        DeletePermissionCascadeCommand command, CancellationToken cancellationToken);
}

public partial class PermissionService : IPermissionService
{
    private readonly IMapper _mapper;
    private readonly IPermissionDataProvider _permissionDataProvider;
    private readonly IRolePermissionService _rolePermissionService;
    private readonly IPermissionMenuService _permissionMenuService;
    private readonly IPermissionEndpointService _permissionEndpointService;

    public PermissionService(
        IMapper mapper,
        IPermissionDataProvider permissionDataProvider,
        IRolePermissionService rolePermissionService,
        IPermissionMenuService permissionMenuService,
        IPermissionEndpointService permissionEndpointService)
    {
        _mapper = mapper;
        _permissionDataProvider = permissionDataProvider;
        _rolePermissionService = rolePermissionService;
        _permissionMenuService = permissionMenuService;
        _permissionEndpointService = permissionEndpointService;
    }

    public async Task<GetPermissionResponse> GetPermissionAsync(
        GetPermissionRequest request, CancellationToken cancellationToken)
    {
        var permission = await _permissionDataProvider.GetPermissionAsync(request.Id, cancellationToken).ConfigureAwait(false);
        
        return new GetPermissionResponse
        {
            Data = _mapper.Map<PermissionDto>(permission)
        };
    }

    public async Task<GetPermissionListResponse> GetPermissionListAsync(
        GetPermissionListRequest request, CancellationToken cancellationToken)
    {
        var list = await _permissionDataProvider.GetPermissionsAsync(request.IdList, cancellationToken).ConfigureAwait(false);
        
        return new GetPermissionListResponse
        {
            Data = _mapper.Map<List<PermissionDto>>(list)
        };
    }

    public async Task<GetPermissionTreeResponse> GetPermissionTreeAsync(
        GetPermissionTreeRequest request, CancellationToken cancellationToken)
    {
        var list = await _permissionDataProvider.GetAllPermissionAsync(cancellationToken).ConfigureAwait(false);

        var tree = list.ConvertTree<Guid, Permission>();

        return new GetPermissionTreeResponse
        {
            Data = _mapper.Map<List<PermissionDto>>(tree)
        };
    }

    public async Task<AddPermissionResponse> AddPermissionAsync(
        AddPermissionCommand command, CancellationToken cancellationToken)
    {
        var response = new AddPermissionResponse();

        if (command.Permission.Pid != Guid.Empty)
        {
            var hasParent = await _permissionDataProvider.GetAnyAsync([command.Permission.Pid], cancellationToken).ConfigureAwait(false);
            if (!hasParent)
            {
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "Parent permission not found";
                return response;
            }
        }

        var permission = _mapper.Map<Permission>(command.Permission);
        
        await _permissionDataProvider.CreatePermissionAsync(permission, cancellationToken).ConfigureAwait(false);
        
        return response;
    }

    public async Task<UpdatePermissionResponse> UpdatePermissionAsync(
        UpdatePermissionCommand command, CancellationToken cancellationToken)
    {
        var response = new UpdatePermissionResponse();

        var permissionSource = await _permissionDataProvider.GetPermissionAsync(command.Permission.Id, cancellationToken).ConfigureAwait(false);
        if (permissionSource == null)
        {
            response.Code = HttpStatusCode.BadRequest;
            response.Message = "Permission not found";
            return response;
        }
        
        if (command.Permission.Pid != Guid.Empty)
        {
            var hasParent = await _permissionDataProvider.GetAnyAsync([command.Permission.Pid], cancellationToken).ConfigureAwait(false);
            if (!hasParent)
            {
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "Parent permission not found";
                return response;
            }
        }

        if (permissionSource.Type == PermissionTypeEnum.Module && command.Permission.Type == PermissionTypeEnum.Action)
        {
            var hasChild = await _permissionDataProvider.GetChildAnyAsync([command.Permission.Id], cancellationToken).ConfigureAwait(false);
            if (hasChild)
            {
                response.Code = HttpStatusCode.BadRequest;
                response.Message = "Child permission exists, cannot change type to Action";
                return response;
            }
        }
        
        var permission = _mapper.Map<Permission>(command.Permission);
        
        await _permissionDataProvider.UpdatePermissionAsync(permission, cancellationToken).ConfigureAwait(false);
        
        return response;
    }

    public async Task<DeletePermissionCascadeResponse> DeletePermissionCascadeAsync(
        DeletePermissionCascadeCommand command, CancellationToken cancellationToken)
    {
        var response = new DeletePermissionCascadeResponse();
        
        var hasChild = await _permissionDataProvider.GetChildAnyAsync(command.IdList, cancellationToken).ConfigureAwait(false);
        if (hasChild)
        {
            response.Code = HttpStatusCode.BadRequest;
            response.Message = "Child permission exists";
            return response;
        }
        
        // Remove permission-role data
        await _rolePermissionService.RemovePermissionCascadeRoleAsync(
            new RemovePermissionCascadeRoleCommand { PermissionIds = command.IdList }, cancellationToken).ConfigureAwait(false);
        
        // Remove permission-menu data
        await _permissionMenuService.RemovePermissionsCascadeMenuAsync(
            new RemovePermissionsCascadeMenuCommand { PermissionIds = command.IdList }, cancellationToken).ConfigureAwait(false);
        
        // Remove permission-endpoint data
        await _permissionEndpointService.RemovePermissionsCascadeEndpointAsync(
            new RemovePermissionsCascadeEndpointCommand { PermissionIds = command.IdList }, cancellationToken).ConfigureAwait(false);
        
        await _permissionDataProvider.DeletePermissionsAsync(command.IdList, cancellationToken).ConfigureAwait(false);
        
        return response;
    }
}
using System.Net;
using AutoMapper;
using TemplateProject.Core.Domain.Security;
using TemplateProject.Core.Extension;
using TemplateProject.Message.Commands.Security;
using TemplateProject.Message.Dto.Security;
using TemplateProject.Message.Request.Security;

namespace TemplateProject.Core.Services.Security;

public interface IMenuService : IScope
{
    public Task<GetMenuResponse> GetMenuAsync(
        GetMenuRequest request, CancellationToken cancellationToken);
    
    public Task<GetMenuListResponse> GetMenuListAsync(
        GetMenuListRequest request, CancellationToken cancellationToken);
    
    public Task<GetMenuTreeResponse> GetMenuTreeAsync(
        GetMenuTreeRequest request, CancellationToken cancellationToken);
    
    public Task<AddMenuResponse> AddMenuAsync(
        AddMenuCommand command, CancellationToken cancellationToken);
    
    public Task<UpdateMenuResponse> UpdateMenuAsync(
        UpdateMenuCommand command, CancellationToken cancellationToken);
    
    public Task<DeleteMenuCascadeResponse> DeleteMenuCascadeAsync(
        DeleteMenuCascadeCommand command, CancellationToken cancellationToken);
}

public class MenuService : IMenuService
{
    private readonly IMapper _mapper;
    private readonly MenuDataProvider _menuDataProvider;
    private readonly IPermissionMenuService _permissionMenuService;

    public MenuService(
        IMapper mapper,
        MenuDataProvider menuDataProvider,
        IPermissionMenuService permissionMenuService)
    {
        _mapper = mapper;
        _menuDataProvider = menuDataProvider;
        _permissionMenuService = permissionMenuService;
    }

    public async Task<GetMenuResponse> GetMenuAsync(
        GetMenuRequest request, CancellationToken cancellationToken)
    {
        var menu = await _menuDataProvider.GetMenuAsync(request.Id, cancellationToken).ConfigureAwait(false);

        return new GetMenuResponse
        {
            Data = _mapper.Map<MenuDto>(menu)
        };
    }

    public async Task<GetMenuListResponse> GetMenuListAsync(
        GetMenuListRequest request, CancellationToken cancellationToken)
    {
        var menuList = await _menuDataProvider.GetMenuListAsync(request.IdList, cancellationToken).ConfigureAwait(false);

        return new GetMenuListResponse
        {
            Data = _mapper.Map<List<MenuDto>>(menuList)
        };
    }

    public async Task<GetMenuTreeResponse> GetMenuTreeAsync(
        GetMenuTreeRequest request, CancellationToken cancellationToken)
    {
        var list = await _menuDataProvider.GetAllMenuAsync(cancellationToken).ConfigureAwait(false);

        var tree = list.ConvertTree<Guid, Menu>();

        return new GetMenuTreeResponse
        {
            Data = _mapper.Map<List<MenuDto>>(tree)
        };
    }

    public async Task<AddMenuResponse> AddMenuAsync(
        AddMenuCommand command, CancellationToken cancellationToken)
    {
        var response = new AddMenuResponse();
        
        var hasParent = await _menuDataProvider.GetAnyAsync([command.Menu.Pid], cancellationToken).ConfigureAwait(false);
        if (!hasParent)
        {
            response.Code = HttpStatusCode.BadRequest;
            response.Message = "Parent menu not found";
            return response;
        }
        
        var menu = _mapper.Map<Menu>(command.Menu);
        
        await _menuDataProvider.CreateMenuAsync(menu, cancellationToken).ConfigureAwait(false);

        return response;
    }

    public async Task<UpdateMenuResponse> UpdateMenuAsync(
        UpdateMenuCommand command, CancellationToken cancellationToken)
    {
        var response = new UpdateMenuResponse();
        
        var menuSource = await _menuDataProvider.GetMenuAsync(command.Menu.Id, cancellationToken).ConfigureAwait(false);
        if (menuSource == null)
        {
            response.Code = HttpStatusCode.BadRequest;
            response.Message = "Menu not found";
            return response;
        }
        
        var hasParent = await _menuDataProvider.GetAnyAsync([command.Menu.Pid], cancellationToken).ConfigureAwait(false);
        if (!hasParent)
        {
            response.Code = HttpStatusCode.BadRequest;
            response.Message = "Parent menu not found";
            return response;
        }
        
        var menu = _mapper.Map<Menu>(command.Menu);
        
        await _menuDataProvider.UpdateMenuAsync(menu, cancellationToken).ConfigureAwait(false);

        return response;
    }

    public async Task<DeleteMenuCascadeResponse> DeleteMenuCascadeAsync(
        DeleteMenuCascadeCommand command, CancellationToken cancellationToken)
    {
        var response = new DeleteMenuCascadeResponse();
        
        var hasChild = await _menuDataProvider.GetChildAnyAsync(command.IdList, cancellationToken).ConfigureAwait(false);
        if (hasChild)
        {
            response.Code = HttpStatusCode.BadRequest;
            response.Message = "Child role exists";
            return response;
        }
        
        // Remove menu-permission data
        await _permissionMenuService.RemoveMenusCascadePermissionAsync(
            new RemoveMenusCascadePermissionCommand { MenuIds = command.IdList }, cancellationToken).ConfigureAwait(false);
        
        await _menuDataProvider.DeleteMenusAsync(command.IdList, cancellationToken).ConfigureAwait(false);

        return response;
    }
}
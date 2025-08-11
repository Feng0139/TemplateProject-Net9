using Microsoft.EntityFrameworkCore;
using TemplateProject.Core.Data;
using TemplateProject.Core.Domain.Security;

namespace TemplateProject.Core.Services.Security;

public interface IMenuDataProvider : IScope
{
    public Task<Menu?> GetMenuAsync(Guid id, CancellationToken cancellationToken);
    
    public Task<List<Menu>> GetMenuListAsync(List<Guid> idList, CancellationToken cancellationToken);
    
    public Task<List<Menu>> GetAllMenuAsync(CancellationToken cancellationToken);
    
    public Task CreateMenuAsync(Menu entity, CancellationToken cancellationToken);
    
    public Task UpdateMenuAsync(Menu entity, CancellationToken cancellationToken);
    
    public Task DeleteMenusAsync(List<Guid> idList, CancellationToken cancellationToken);
    
    public Task<bool> GetAnyAsync(List<Guid> idList, CancellationToken cancellationToken);
    
    public Task<bool> GetChildAnyAsync(List<Guid> pidList, CancellationToken cancellationToken);
}

public class MenuDataProvider : IMenuDataProvider
{
    private readonly IRepository<Menu> _menuRep;
    
    public MenuDataProvider(IRepository<Menu> menuRep)
    {
        _menuRep = menuRep;
    }
    
    public async Task<Menu?> GetMenuAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _menuRep.QueryNoTracking(x => x.Id == id).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<List<Menu>> GetMenuListAsync(List<Guid> idList, CancellationToken cancellationToken)
    {
        return await _menuRep.QueryNoTracking(x => idList.Contains(x.Id)).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<List<Menu>> GetAllMenuAsync(CancellationToken cancellationToken)
    {
        return await _menuRep.QueryNoTracking().ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task CreateMenuAsync(Menu entity, CancellationToken cancellationToken)
    {
        await _menuRep.InsertAsync(entity, cancellationToken).ConfigureAwait(false);
    }

    public async Task UpdateMenuAsync(Menu entity, CancellationToken cancellationToken)
    {
        await _menuRep.UpdateAsync(entity, cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteMenusAsync(List<Guid> idList, CancellationToken cancellationToken)
    {
        await _menuRep.DeleteAsync(x => idList.Contains(x.Id), cancellationToken).ConfigureAwait(false);
    }

    public async Task<bool> GetAnyAsync(List<Guid> idList, CancellationToken cancellationToken)
    {
        return await _menuRep.AnyAsync(x => idList.Contains(x.Id), cancellationToken).ConfigureAwait(false);
    }

    public async Task<bool> GetChildAnyAsync(List<Guid> pidList, CancellationToken cancellationToken)
    {
        return await _menuRep.AnyAsync(x => pidList.Contains(x.Pid), cancellationToken).ConfigureAwait(false);
    }
}
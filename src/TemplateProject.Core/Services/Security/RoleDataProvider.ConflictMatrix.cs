using Microsoft.EntityFrameworkCore;
using TemplateProject.Core.Domain.Security;

namespace TemplateProject.Core.Services.Security;

public partial interface IRoleDataProvider : IScope
{
    public Task<List<RoleConflictMatrix>> GetRoleConflictMatrixListAsync(
        List<Guid> idList, CancellationToken cancellationToken = default);
    
    public Task AddRoleConflictMatrixAsync(
        Guid roleAId, Guid roleBId, CancellationToken cancellationToken = default);
    
    public Task DeleteRoleConflictMatrixAsync(
        List<Guid> idList, CancellationToken cancellationToken = default);
}

public partial class RoleDataProvider : IRoleDataProvider
{
    public async Task<List<RoleConflictMatrix>> GetRoleConflictMatrixListAsync(
        List<Guid> idList, CancellationToken cancellationToken = default)
    {
        var query = _roleConflictMatrixRep.QueryNoTracking()
            .Where(x => idList.Contains(x.RoleAId) || idList.Contains(x.RoleBId));
        
        var list = await query.ToListAsync(cancellationToken).ConfigureAwait(false);

        return list;
    }

    public async Task AddRoleConflictMatrixAsync(
        Guid roleAId, Guid roleBId, CancellationToken cancellationToken = default)
    {
        var roleConflictMatrix = new RoleConflictMatrix
        {
            RoleAId = roleAId,
            RoleBId = roleBId
        };

        await _roleConflictMatrixRep.InsertAsync(roleConflictMatrix, cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteRoleConflictMatrixAsync(
        List<Guid> idList, CancellationToken cancellationToken = default)
    {
        await _roleConflictMatrixRep.DeleteAsync(x => idList.Contains(x.Id), cancellationToken).ConfigureAwait(false);
    }
}
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TemplateProject.Message.Dto;

namespace TemplateProject.Core.Data;

public class Repository<TEntity>(TemplateProjectDbContext dbContext) : IRepository<TEntity> where TEntity : class, IEntityBase
{
    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TEntity>().SingleOrDefaultAsync(predicate, cancellationToken).ConfigureAwait(false);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken).ConfigureAwait(false);
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TEntity>().ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await dbContext.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        dbContext.ShouldSaveChanges = true;
    }

    public async Task InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await dbContext.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        dbContext.ShouldSaveChanges = true;
    }

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        dbContext.Update(entity);
        dbContext.ShouldSaveChanges = true;
        return Task.CompletedTask;
    }

    public Task UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        dbContext.UpdateRange(entities);
        dbContext.ShouldSaveChanges = true;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        dbContext.Remove(entity);
        dbContext.ShouldSaveChanges = true;
        return Task.CompletedTask;
    }

    public Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        dbContext.RemoveRange(entities);
        dbContext.ShouldSaveChanges = true;
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var entities = await dbContext.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken).ConfigureAwait(false);
        if (entities.Count != 0)
        {
            dbContext.RemoveRange(entities);
            dbContext.ShouldSaveChanges = true;
        }
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TEntity>().CountAsync(predicate, cancellationToken).ConfigureAwait(false);
    }
    
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TEntity>().AnyAsync(predicate, cancellationToken).ConfigureAwait(false);
    }

    public async Task<List<TEntity>> SqlQueryAsync(string sql, params object[] parameters)
    {
        return await dbContext.Set<TEntity>().FromSqlRaw(sql, parameters).ToListAsync().ConfigureAwait(false);
    }

    public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate == null ? dbContext.Set<TEntity>() : dbContext.Set<TEntity>().Where(predicate);
    }

    public IQueryable<TEntity> QueryNoTracking(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate == null
            ? dbContext.Set<TEntity>().AsNoTracking()
            : dbContext.Set<TEntity>().AsNoTracking().Where(predicate);
    }
}
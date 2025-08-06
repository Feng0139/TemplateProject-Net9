using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TemplateProject.Core.Services.Identity;
using TemplateProject.Core.Settings.System;
using TemplateProject.Message.Dto;

namespace TemplateProject.Core.Data;

public class TemplateProjectDbContext(ConnectionStringSetting connectionString, ICurrentUser currentUser) : DbContext, IUnitOfWork
{
    private readonly string _dbConnectionString = connectionString.Mysql;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseMySql(_dbConnectionString, new MySqlServerVersion(new Version(8, 0, 3)))
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .UseSnakeCaseNamingConvention();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        RegisterAllEntities(modelBuilder);
    }
    
    private void RegisterAllEntities(ModelBuilder modelBuilder)
    {
        var entityTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => !t.IsAbstract &&
                        t.IsClass &&
                        typeof(IEntityBase).IsAssignableFrom(t) &&
                        modelBuilder.Model.FindEntityType(t) == null)
            .ToList();

        foreach (var type in entityTypes)
        {
            modelBuilder.Model.AddEntityType(type);
        }
    }
    
    public bool ShouldSaveChanges { get; set; }
    
    public override int SaveChanges()
    {
        UpdateEntityAudit();
        UpdateCreatedFields();
        UpdateLastModifiedFields();
        return ShouldSaveChanges ? base.SaveChanges() : 0;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateEntityAudit();
        UpdateCreatedFields();
        UpdateLastModifiedFields();
        return ShouldSaveChanges ? base.SaveChangesAsync(cancellationToken) : Task.FromResult(0);
    }

    private void UpdateEntityAudit()
    {
        var userId = currentUser.Id ?? Guid.Empty;

        foreach (var entry in ChangeTracker.Entries()
                     .Where(e => e.Entity.GetType().GetInterfaces()
                         .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityAudit<>))))
        {
            dynamic entity = entry.Entity;
            
            switch (entry.State)
            {
                case EntityState.Added:
                    entity.CreatedBy = userId;
                    break;
                case EntityState.Modified:
                    entity.LastModifiedBy = userId;
                    break;
            }
        }
    }
    
    private void UpdateCreatedFields()
    {
        foreach (var entry in ChangeTracker.Entries().Where(e => e is { State: EntityState.Added, Entity: IEntityCreated }))
        {
            if (((IEntityCreated)entry.Entity).CreatedAt == default)
            {
                ((IEntityCreated)entry.Entity).CreatedAt = DateTimeOffset.UtcNow;
            }
        }
    }
    
    private void UpdateLastModifiedFields()
    {
        foreach (var entry in ChangeTracker.Entries().Where(e => e is { State: EntityState.Modified, Entity: IEntityModified }))
        {
            ((IEntityModified)entry.Entity).UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
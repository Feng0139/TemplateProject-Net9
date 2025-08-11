using Autofac;
using TemplateProject.Core.Data;

namespace TemplateProject.IntegrationTest;

[Collection("Sequential")]
public class IntegrationFixture : IAsyncLifetime
{
    private readonly ILifetimeScope _lifetimeScope;
    private readonly Func<Task> _resetDatabase;
    
    public IntegrationFixture(IntegrationTestBase testBase)
    {
        _lifetimeScope = testBase.LifetimeScope;
        _resetDatabase = testBase.ResetDatabaseAsync;
    }
    
    protected async Task Run<T>(Func<T, Task> action, Action<ContainerBuilder>? extraRegistration = null)
        where T : notnull
    {
        var dependency = extraRegistration != null
            ? _lifetimeScope.BeginLifetimeScope(extraRegistration).Resolve<T>()
            : _lifetimeScope.BeginLifetimeScope().Resolve<T>();
     
        await action(dependency);
    }
    
    protected async Task Run<T, U>(Func<T, U, Task> action, Action<ContainerBuilder>? extraRegistration = null)
        where T : notnull
        where U : notnull
    {
        var lifetime = extraRegistration != null
            ? _lifetimeScope.BeginLifetimeScope(extraRegistration)
            : _lifetimeScope.BeginLifetimeScope();
        
        var dependency = lifetime.Resolve<T>();
        var dependency2 = lifetime.Resolve<U>();
        
        await action(dependency, dependency2);
    }
    
    protected async Task RunWithUnitOfWork<T>(Func<T, Task> action, Action<ContainerBuilder>? extraRegistration = null)
        where T : notnull
    {
        var scope = extraRegistration != null
            ? _lifetimeScope.BeginLifetimeScope(extraRegistration)
            : _lifetimeScope.BeginLifetimeScope();

        var dependency = scope.Resolve<T>();
        var unitOfWork = scope.Resolve<IUnitOfWork>();
        
        await action(dependency);
        await unitOfWork.SaveChangesAsync();
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        return _resetDatabase();
    }
}
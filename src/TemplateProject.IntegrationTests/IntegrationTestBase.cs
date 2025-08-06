using System.Text.Json.Nodes;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using NSubstitute;
using Respawn;
using Serilog;
using TemplateProject.Core;
using TemplateProject.Core.Data;
using TemplateProject.Core.Dbup;
using TemplateProject.Core.Settings;
using TemplateProject.Core.Settings.System;

namespace TemplateProject.IntegrationTest;

public class IntegrationTestBase : IAsyncLifetime
{
    private const string Topic = "integration_test";
    
    public readonly ILifetimeScope LifetimeScope;
    
    private IConfiguration Configuration => LifetimeScope.Resolve<IConfiguration>();
    
    private Respawner _respawner;

    public IntegrationTestBase()
    {
        var logger = Substitute.For<ILogger>();
        
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging(l =>
        {
            l.AddSerilog();
        });
        
        var containerBuilder = new ContainerBuilder();
        
        var configuration = RegisterConfiguration(containerBuilder);
        
        containerBuilder.Populate(serviceCollection);
        containerBuilder.RegisterModule(new CoreModule(new IntegrationTestUser(), logger, configuration, typeof(IntegrationFixture).Assembly));
        containerBuilder.RegisterInstance(new MemoryCache(new MemoryCacheOptions())).AsSelf().As<IMemoryCache>().SingleInstance();
        
        LifetimeScope = containerBuilder.Build();
        
        new DbUpRunner(new ConnectionStringSetting(Configuration).Mysql).Run("Dbup");
    }
    
    private IConfiguration RegisterConfiguration(ContainerBuilder containerBuilder)
    {
        var targetJson = $"appsetting_{Topic}.json";
        File.Copy("appsettings.json", targetJson, true);

        var document = JsonNode.Parse(File.ReadAllText(targetJson)) ?? throw new InvalidOperationException();
        document["ConnectionStrings"]!["Mysql"] = document["ConnectionStrings"]!["Mysql"]!.ToString()
            .Replace("database=db_template_project", $"database=db_template_project_{Topic}");
        
        File.WriteAllText(targetJson, document.ToJsonString());
        
        var configuration = new ConfigurationBuilder().AddJsonFile(targetJson).Build();
        containerBuilder.RegisterInstance(configuration).AsImplementedInterfaces();
        
        configuration.Get(typeof(AppSetting));
        
        return configuration;
    }
    
    public async Task ResetDatabaseAsync()
    {
        await using var conn = new MySqlConnection(new ConnectionStringSetting(Configuration).Mysql);
        await conn.OpenAsync();
        await _respawner.ResetAsync(conn);
    }

    public async Task InitializeAsync()
    {
        await using var conn = new MySqlConnection(new ConnectionStringSetting(Configuration).Mysql);
        await conn.OpenAsync();
        
        _respawner = await Respawner.CreateAsync(conn, new RespawnerOptions
        {
            TablesToIgnore =
            [
                "schemaversions"
            ],
            SchemasToInclude =
            [
                $"db_template_project_{Topic}"
            ],
            DbAdapter = DbAdapter.MySql
        });
    }

    public Task DisposeAsync()
    {
        var context = LifetimeScope.Resolve<TemplateProjectDbContext>();
        return context.Database.EnsureDeletedAsync();
    }
}
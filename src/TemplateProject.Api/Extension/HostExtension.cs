using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.MemoryStorage;
using Hangfire.Redis.StackExchange;
using Scalar.AspNetCore;
using Serilog;
using TemplateProject.Api.ActionFilters;
using TemplateProject.Core;
using TemplateProject.Core.Extension;
using TemplateProject.Core.Settings;
using TemplateProject.Core.Settings.System;
using TemplateProject.Message.Enum;

namespace TemplateProject.Api.Extension;

public static class HostExtension
{
    public static WebApplicationBuilder ConfigureHost(this WebApplicationBuilder builder)
    {
        var host = builder.Host;
        var configuration = builder.Configuration;

        host.UseSerilog();
        host.UseSerilog(Log.Logger);
        host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterModule(new CoreModule(null, Log.Logger, configuration, typeof(CoreModule).Assembly));
        });
        
        return builder;
    }
    
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;
        
        var cacheSetting = new CacheSetting(configuration);
        var hangfireSetting = new HangfireSetting(configuration);
        var connectionSetting = new ConnectionStringSetting(configuration);
        
        services.AddHttpContextAccessor();
        
        services.AddOpenApi();

        services.AddControllers(options =>
        {
            options.Filters.Add<UnifyResponseFilter>();
            options.Filters.Add<UnifyResponseExceptionFilter>();
        });
        
        services.AddHangfire(config =>
        {
            switch(hangfireSetting.UseStorage)
            {
                case CacheTypeEnum.Redis:
                    var option = new RedisStorageOptions { Db = cacheSetting.RedisDatabaseIndex };
                    config.UseRedisStorage(connectionSetting.Redis, option);
                    break;
                case CacheTypeEnum.Memory:
                default:
                    config.UseMemoryStorage();
                    break;
            }
        });
        services.AddHangfireServer();

        return builder;
    }
    
    public static WebApplication ConfigureApp(this WebApplication app)
    {
        var configuration = app.Configuration;
        var hangfireSetting = new HangfireSetting(configuration);
        
        if (AppSetting.EnableScalar)
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
        
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        
        if (hangfireSetting.EnableDashboard)
        {
            var option = new DashboardOptions
            {
                Authorization = [
                    new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                    {
                        SslRedirect = false,
                        RequireSsl = false,
                        LoginCaseSensitive = true,
                        Users =
                        [
                            new BasicAuthAuthorizationUser
                            {
                                Login = hangfireSetting.DashboardUser,
                                PasswordClear = hangfireSetting.DashboardPassword
                            }
                        ]
                    })
                ]
            };
            
            app.UseHangfireDashboard(hangfireSetting.DashboardPath, option);
        }
        app.UseForgetJobs();
        app.UseDelayedJobs();
        app.UseRecurringJobs();

        return app;
    }
}
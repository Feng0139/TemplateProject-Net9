using System.Reflection;
using Autofac;
using Autofac.Core;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Mediator.Net;
using Mediator.Net.Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using StackExchange.Redis;
using TemplateProject.Core.Caching;
using TemplateProject.Core.Data;
using TemplateProject.Core.Jobs;
using TemplateProject.Core.Middlewares.FluentMessageValidator;
using TemplateProject.Core.Middlewares.Logger;
using TemplateProject.Core.Middlewares.MessageLogger;
using TemplateProject.Core.Middlewares.UnifyResponse;
using TemplateProject.Core.Middlewares.UnitofWork;
using TemplateProject.Core.Services;
using TemplateProject.Core.Services.Identity;
using TemplateProject.Core.Settings;
using TemplateProject.Core.Settings.System;
using TemplateProject.Message.Enum;
using Module = Autofac.Module;

namespace TemplateProject.Core;

public class CoreModule(ICurrentUser? currentUser, ILogger logger, IConfiguration configuration, params Assembly[] assemblies) : Module
{
    private readonly Assembly[] _assemblies = assemblies.Length == 0
        ? [typeof(CoreModule).Assembly]
        : assemblies.Concat([typeof(CoreModule).Assembly]).Distinct().ToArray();
    
    protected override void Load(ContainerBuilder builder)
    {
        RegisterIdentity(builder);
        
        RegisterLogger(builder);
        
        RegisterMediator(builder);

        RegisterAutoMapper(builder);

        RegisterDependency(builder);
        
        RegisterCaching(builder);
        
        RegisterSettings(builder);

        RegisterDbContext(builder);
        
        RegisterValidator(builder);

        RegisterJobs(builder);
    }
    
    // 注册身份代理
    private void RegisterIdentity(ContainerBuilder builder)
    {
        if (!builder.ComponentRegistryBuilder.IsRegistered(new TypedService(typeof(IHttpContextAccessor))))
        {
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
        }
        
        builder.Register(c =>
        {
            var provider = c.Resolve<ICurrentUserProvider>();
            if (provider.CurrentUser != null)
                return new CurrentUserProxy(provider);
            
            if (currentUser != null)
            {
                provider.SetCurrentUser(currentUser);
            }
            else
            {
                var httpContext = c.Resolve<IHttpContextAccessor>();
                var httpContextUser = new HttpCurrentUser(httpContext);
                provider.SetCurrentUser(httpContextUser);
            }

            return new CurrentUserProxy(provider);
        }).As<ICurrentUser>().InstancePerLifetimeScope();
    }
    
    // 注册日志
    private void RegisterLogger(ContainerBuilder builder)
    {
        builder.RegisterInstance(logger).AsSelf().AsImplementedInterfaces().SingleInstance();
    }
    
    // 注册中介者，用于解耦，后续调用 Service 请使用 IMediator
    private void RegisterMediator(ContainerBuilder builder)
    {
        var mediatorBuilder = new MediatorBuilder();

        mediatorBuilder.RegisterHandlers(_assemblies);
        mediatorBuilder.ConfigureGlobalReceivePipe(p =>
        {
            p.UseLogger();          // 统一日志记录
            p.UseMessageLogging();  // 特殊标记日志记录
            p.UseMessageValidator();// 消息验证
            p.UseUnitOfWork();      // 事务处理
            p.UseUnifyResponse();   // 统一响应
        });

        builder.RegisterMediator(mediatorBuilder);
    }
    
    // 注册 AutoMapper，用于对象映射
    private void RegisterAutoMapper(ContainerBuilder builder)
    {
        builder.RegisterAutoMapper(false, _assemblies);
    }
    
    // 注册依赖注入
    private void RegisterDependency(ContainerBuilder builder)
    {
        var allServiceTypes = _assemblies.SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && t.GetInterfaces().Any(i => i == typeof(IService)))
            .ToList();

        foreach (var type in allServiceTypes)
        {
            var registration = builder.RegisterType(type).AsImplementedInterfaces();

            if (typeof(ISingleton).IsAssignableFrom(type))
            {
                registration.SingleInstance();
            }
            else if (typeof(IScope).IsAssignableFrom(type))
            {
                registration.InstancePerLifetimeScope();
            }
        }
    }
    
    // 注册缓存
    private void RegisterCaching(ContainerBuilder builder)
    {
        var cacheSetting = new CacheSetting(configuration);

        if (cacheSetting.UseStorage == CacheTypeEnum.Redis)
        {
            // 注册 Redis 缓存
            builder.Register(context =>
            {
                var connection = ConnectionMultiplexer.Connect(cacheSetting.RedisConnection);
                return new RedisCacheManager(connection, cacheSetting.RedisDatabaseIndex);
            }).As<ICacheManager>().SingleInstance();
        }
        else
        {
            // 注册 Memory 缓存
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().SingleInstance();
        }
    }
    
    // 注册配置
    private void RegisterSettings(ContainerBuilder builder)
    {
        var settingTypes = _assemblies.SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && typeof(IConfigurationSetting).IsAssignableFrom(t))
            .ToArray();

        builder.RegisterTypes(settingTypes).AsSelf().SingleInstance();
    }
    
    // 注册数据库上下文
    private void RegisterDbContext(ContainerBuilder builder)
    {
        builder.RegisterType<TemplateProjectDbContext>()
            .AsSelf()
            .As<DbContext>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
    }
    
    // 注册消息验证器
    private void RegisterValidator(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(_assemblies)
            .Where(t => t.IsClass && typeof(IFluentMessageValidator).IsAssignableFrom(t))
            .AsSelf().AsImplementedInterfaces();
    }
    
    // 注册任务
    private void RegisterJobs(ContainerBuilder builder)
    {
        foreach (var type in typeof(IJob).Assembly.GetTypes()
                     .Where(t => typeof(IJob).IsAssignableFrom(t) && t.IsClass))
        {
            builder.RegisterType(type).AsSelf().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
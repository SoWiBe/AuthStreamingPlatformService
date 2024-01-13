using System.IdentityModel.Tokens.Jwt;
using Autofac;
using Autofac.Configuration;
using Microsoft.AspNetCore.Diagnostics;
using StreamingPlatformService.Infrastructure.Abstractions.Repositories;
using StreamingPlatformService.Infrastructure.Data;
using StreamingPlatformService.Infrastructure.Repositories;

namespace StreamingPlatformService.Infrastructure;

public class DefaultInfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterCommonDependencies(builder);
    }

    private void RegisterCommonDependencies(ContainerBuilder builder)
    {
        builder.RegisterType<ExceptionHandlerMiddleware>().AsSelf().InstancePerLifetimeScope();

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        builder.RegisterGeneric(typeof(RepositoryBase<>))
            .As(typeof(IRepository<>))
            .As(typeof(IReadRepository<>))
            .InstancePerLifetimeScope();

        builder.Register(c =>
        {
            var config = c.Resolve<IConfiguration>();
            
            var context = new AppMongoDbContext(config.GetConnectionString("Mongo"));
            return context;
        })
        .AsSelf()
        .As<IAppMongoDbContext>()
        .InstancePerLifetimeScope();

        var module = new ConfigurationModule(GetConfiguration());
        builder.RegisterModule(module);
    }

    private static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder();

        builder.AddJsonFile("appsettings.json", true, true);

#if DEBUG
        builder.AddJsonFile("appsettings.Development.json", true, true);
#endif

        return builder.Build();
    }
}
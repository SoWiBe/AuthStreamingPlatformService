using System.IdentityModel.Tokens.Jwt;
using Autofac;
using Autofac.Configuration;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TechDaily.Infrastructure.Data;

namespace TechDaily.Infrastructure;

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

        builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();

                var opt = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlite(config.GetConnectionString("Default"));

                var context = new AppDbContext(opt.Options);
                context.Database.EnsureCreated();

                return context;
            }).AsSelf()
            .As<DbContext>()
            .InstancePerLifetimeScope();

        // builder.RegisterGeneric(typeof(RepositoryBase<>))
        //     .As(typeof(IRepository<>))
        //     .As(typeof(IReadRepository<>))
        //     .InstancePerLifetimeScope();
        // builder.RegisterType<ConfigurationRepository>().As<IConfigurationRepository>();
        // builder.RegisterType<ApiRepository>().As<IApiRepository>();
        // builder.RegisterType<JsonRepository>().As<IJsonRepository>();
        //
        // builder.RegisterType<DataConvertor>().As<IConvertor>();
        // builder.RegisterType<LoggerService>().As<ILoggerService>();
        //
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
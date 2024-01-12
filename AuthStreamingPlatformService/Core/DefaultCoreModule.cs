using AuthStreamingPlatformService.Core.Abstractions.Services;
using AuthStreamingPlatformService.Core.Services;
using Autofac;

namespace AuthStreamingPlatformService.Core;

public class DefaultCoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UsersService>().As<IUsersService>();
    }
}
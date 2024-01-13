using Autofac;
using StreamingPlatformService.Core.Abstractions.Services;
using StreamingPlatformService.Core.Services;

namespace StreamingPlatformService.Core;

public class DefaultCoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UsersService>().As<IUsersService>();
        builder.RegisterType<ClaimsService>().As<IClaimsService>();
        builder.RegisterType<JwtTokenService>().As<IJwtTokenService>();
    }
}
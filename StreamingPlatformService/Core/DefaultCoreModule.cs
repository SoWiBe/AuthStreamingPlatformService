using Autofac;
using StreamingPlatformService.Core.Abstractions.Services;
using StreamingPlatformService.Core.Abstractions.Services.Categories;
using StreamingPlatformService.Core.Abstractions.Services.Users;
using StreamingPlatformService.Core.Services;
using StreamingPlatformService.Core.Services.Categories;

namespace StreamingPlatformService.Core;

public class DefaultCoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UsersService>().As<IUsersService>();
        builder.RegisterType<CategoriesService>().As<ICategoriesService>();
        
        builder.RegisterType<ClaimsService>().As<IClaimsService>();
        builder.RegisterType<JwtTokenService>().As<IJwtTokenService>();
    }
}
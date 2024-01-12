using AuthStreamingPlatformService.Entities.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace AuthStreamingPlatformService.Entities;

public class Role : IdentityUser<Guid>
{
    public Role(string name)
    {
        UserName = name;
    }
}
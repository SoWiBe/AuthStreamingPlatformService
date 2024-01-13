using Microsoft.AspNetCore.Identity;

namespace StreamingPlatformService.Entities;

public class Role : IdentityUser<Guid>
{
    public Role(string name)
    {
        UserName = name;
    }
}
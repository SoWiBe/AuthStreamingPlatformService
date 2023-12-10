using AuthStreamingPlatformService.Entities.Abstractions;

namespace AuthStreamingPlatformService.Entities;

public class User : IEntityBase<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
}
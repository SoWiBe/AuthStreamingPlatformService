using MongoDB.Driver;

namespace AuthStreamingPlatformService.Entities.Responses;

public class GetUsersResponse
{
    public IEnumerable<User> Users { get; set; }
}
namespace StreamingPlatformService.Entities.Responses;

public class GetUsersResponse
{
    public IEnumerable<User> Users { get; set; }
}
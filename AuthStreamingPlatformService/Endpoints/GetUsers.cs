using AuthStreamingPlatformService.Entities;
using AuthStreamingPlatformService.Entities.Responses;
using AuthStreamingPlatformService.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TechDaily.Infrastructure.Endpoints;

namespace AuthStreamingPlatformService.Endpoints;

public class GetUsers : EndpointBaseAsync.WithoutRequest.WithActionResult<GetUsersResponse>
{
    private readonly IAppMongoDbContext _context;

    public GetUsers(IAppMongoDbContext context)
    {
        _context = context;
    }
    
    [AllowAnonymous]
    [HttpGet("/users")]
    public override async Task<ActionResult<GetUsersResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var result = _context.GetDatabase();

        var users = result.GetCollection<User>("Users");
        var user = new User
        {
            Nick = "test", 
            Email = "test", Logo = 1,
            FirstName = "tt", 
            LastName = "tt", 
            Password = "123"
        };
        
        await users.InsertOneAsync(user);

        var documents = await users.Find(_ => true).ToListAsync(cancellationToken);

        return Ok(new GetUsersResponse { Users = documents });
    }
}
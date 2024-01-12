using System.ComponentModel.DataAnnotations;
using AuthStreamingPlatformService.Entities.Abstractions;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuthStreamingPlatformService.Entities;

public class User : IdentityUser<Guid>
{
    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [BsonElement("nick")]
    public string Nick { get; set; }
    
    [Required]
    [BsonElement("first_name")]
    public string FirstName { get; set; }
    
    [Required]
    [BsonElement("last_name")]
    public string LastName { get; set; }
    
    [Required]
    [BsonElement("email")]
    public string Email { get; set; }
    
    [Required]
    [BsonElement("password")]
    public string Password { get; set; }
    
    [Required]
    [BsonElement("logo")]
    public int Logo { get; set; }
}
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using StreamingPlatformService.Entities.Abstractions;

namespace StreamingPlatformService.Entities;

public class Channel : IEntityBase<Guid>
{
    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }
    
    [Required]
    [BsonElement("user")]
    public User User { get; set; }
    
    [Required]
    [BsonElement("subscribers")]
    public IEnumerable<User> Subscribers { get; set; }

    [Required]
    [BsonElement("subscribers_count")]
    public int SubscribersCount { get; set; }
    
    [Required]
    [BsonElement("category")]
    public Category Category { get; set; }
}
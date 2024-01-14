using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using StreamingPlatformService.Entities.Abstractions;

namespace StreamingPlatformService.Entities;

public class Category : IEntityBase<Guid>
{
    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }
    
    [Required]
    [BsonElement("title")]
    public string Title { get; set; }
}
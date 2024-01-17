﻿using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using StreamingPlatformService.Entities.Abstractions;

namespace StreamingPlatformService.Entities;

public class User : IEntityBase<Guid>
{
    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }
    
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
    [BsonElement("subscribes")]
    public IEnumerable<Channel> Subscribes { get; set; }

    [Required]
    [BsonElement("logo")]
    public int Logo { get; set; }
}
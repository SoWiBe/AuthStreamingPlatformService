﻿using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace StreamingPlatformService.Entities.Requests.Channels;

public class SubscribeRequest
{
    public const string Route = "/channel/toggle-subscribe";
    
    [JsonPropertyName("channelId")]
    public Guid ChannelId { get; set; }
}
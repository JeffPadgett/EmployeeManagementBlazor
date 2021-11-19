﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamServices.Dto
{
    public class SubscriptionDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("broadcaster_user_id")]
        public string BroadcasterUserId { get; set; }

    }
}

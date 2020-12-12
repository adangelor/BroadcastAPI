using Newtonsoft.Json;
using System;

namespace BroadcastApi.Auth
{
    public class JwtToken
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("expiration")]
        public DateTime Expiration { get; set; }
    }
}

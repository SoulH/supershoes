using Newtonsoft.Json;

namespace Models
{
    public class TokenModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}

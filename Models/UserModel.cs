using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserModel
    {
        [JsonProperty("email")]
        public string Email { get; set; } = "";
        [JsonProperty("username")]
        public string Username { get; set; } = "Guest";
        [JsonProperty("password")]
        public string Password { get; set; } = "";
        [JsonProperty("confirm_password")]
        public string ConfirmPassword { get; set; } = "";
        [JsonProperty("rememberme")]
        public bool RememberMe { get; set; }
        [JsonIgnore]
        public bool IsAuthenticated { get; set; }
    }
}

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class StoreModel : Model
    {
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required]
        [JsonProperty("address")]
        public string Address { get; set; }
    }
}

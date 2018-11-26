using Newtonsoft.Json;

namespace App.Models
{
    public class Listing
    {
        [JsonProperty("take")]
        public int Take { get; set; } = 50;
        [JsonProperty("skip")]
        public int Skip { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; } = "";
    }

    public class ListArticlesByStore : Listing
    {
        [JsonProperty("store_id")]
        public int StoreId { get; set; }
    }
}
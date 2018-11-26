using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ArticleModel : Model
    {
        [JsonProperty("store_id")]
        [DisplayName("Store")]
        public int StoreId { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [Required]
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [Required]
        [JsonProperty("total_in_shelf")]
        [DisplayName("Total In Shelf")]
        public int TotalInShelf { get; set; }

        [Required]
        [DisplayName("Total In Vault")]
        [JsonProperty("total_in_vault")]
        public int TotalInVault { get; set; }

        [DisplayName("Store Name")]
        [JsonProperty("store_name")]
        public string StoreName { get; set; }
    }
}

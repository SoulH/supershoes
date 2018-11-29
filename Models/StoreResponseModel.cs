using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StoreResponseModel : ResponseModelBase
    {
        [JsonProperty("store")]
        public StoreModel Store { get; set; }
    }
}

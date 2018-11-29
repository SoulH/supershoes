using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StoresResponseModel : ResponseModelBase
    {
        [JsonProperty("stores")]
        public List<StoreModel> Stores { get; set; }
    }
}

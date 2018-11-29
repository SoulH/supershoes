using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ArticleResponseModel : ResponseModelBase
    {
        [JsonProperty("article")]
        public ArticleModel Article { get; set; }
    }
}

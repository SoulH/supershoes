using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ArticlesResponseModel : ResponseModelBase
    {
        [JsonProperty("articles")]
        public List<ArticleModel> Articles { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Store : Base
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual List<Article> Articles { get; set; }

        #region Methods
        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Address));
        }
        #endregion
    }
}

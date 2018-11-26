using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Article : Base
    {
        public int StoreId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int TotalInShelf { get; set; }
        public int TotalInVault { get; set; }

        public virtual Store Store { get; set; }

        #region Methods
        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Name) ||
                string.IsNullOrEmpty(Description) ||
                (Price < 1) || (TotalInShelf < 0) ||
                (TotalInVault < 0) || (StoreId < 1));
        }
        #endregion
    }
}

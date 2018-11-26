using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public abstract class Base
    {
        public int Id { get; set; }
        // Audit
        //public string CreationUser { get; set; }
        //public string LastModificationUser { get; set; }
        //public DateTime? CreationDate { get; set; } = DateTime.Now;
        //public DateTime? LastModificationDate { get; set; } = null;

        //public Base()
        //{
        //    CreationDate = DateTime.Now;
        //}
    }
}

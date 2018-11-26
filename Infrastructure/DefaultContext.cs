using Core.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DefaultContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<Article> Articles { get; set; }

        public DefaultContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static DefaultContext Create()
        {
            return new DefaultContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}

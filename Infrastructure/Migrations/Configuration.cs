namespace Infrastructure.Migrations
{
    using Core.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Infrastructure.DefaultContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Infrastructure.DefaultContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Stores.AddOrUpdate(
                f => f.Id,
                new Store { Id = 1, Name = "Super Store", Address = "Somewhere over the rainbow" }
            );

            context.Articles.AddOrUpdate(
                f => f.Id,
                new Article { Id = 1, Description = "The best quality of shoes in a green color", Name = "green shoes", Price = 20.15M,  TotalInShelf = 25, TotalInVault = 40, StoreId = 1 }
            );
        }
    }
}

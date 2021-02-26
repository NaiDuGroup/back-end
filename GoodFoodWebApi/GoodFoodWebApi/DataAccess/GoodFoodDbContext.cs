using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class GoodFoodDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("SERVER=goodfooddb.postgres.database.azure.com;Database=goodfooddb;Port=5432;Username=goodFood_deploy;Password=Domino3228;SslMode=Require");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
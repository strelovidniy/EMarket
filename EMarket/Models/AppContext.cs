using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EMarket.Models
{
    public class AppContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"emarketdbdbserver.database.windows.net";
            builder.UserID = "EMarketHackaton@emarketdbdbserver";
            builder.Password = "Hackaton0103";
            builder.InitialCatalog = "EMarketDB";
            optionsBuilder.UseSqlServer(builder.ConnectionString);
        }
    }
}

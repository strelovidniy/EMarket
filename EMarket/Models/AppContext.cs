﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EMarket.Models
{
    public class AppContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductOrder>().HasKey(order => new { order.OrderId, order.ProductId });
            modelBuilder.Entity<Buyer>().HasAlternateKey(b => b.Email);
            modelBuilder.Entity<Seller>().HasAlternateKey(b => b.Email);
            modelBuilder.Entity<ProductOrder>().HasOne(po => po.Order).WithMany(o => o.ProductOrder)
                .HasForeignKey(po => po.OrderId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProductOrder>().HasOne(po => po.Product).WithMany(p => p.ProductOrder)
                .HasForeignKey(po => po.ProductId).OnDelete(DeleteBehavior.NoAction);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"mentor-me-dbserver.database.windows.net,1433";
            builder.UserID = "MentorMeDbUser@mentor-me-dbserver";
            builder.Password = "!Mentor123";
            builder.InitialCatalog = "EMarket";
            optionsBuilder.UseSqlServer(builder.ConnectionString);
        }
    }
}

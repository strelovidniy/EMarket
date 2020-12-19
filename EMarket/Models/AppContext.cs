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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder();
           // connection.
            //DbConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
        }
    }
}

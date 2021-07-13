using Microsoft.EntityFrameworkCore;
using MVCTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MVCTest.Repository
{
    public class DBContext : DbContext
    {
        public DbSet<Product> Product { get; set; }

        public DbSet<Order> Order { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(@"C:\inetpub\wwwroot\JaxxWaxx")
               .AddJsonFile("appsettings.json")
               .Build();
            string connectionString = configuration.GetConnectionString("ConnectionStr");
            optionsBuilder.UseSqlServer(connectionString);
            
        }
    }
}

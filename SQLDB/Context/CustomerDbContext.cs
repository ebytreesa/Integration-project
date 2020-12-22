using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace SQLDB.Context
{
    public class CustomerDbContext : DbContext
    {     
        public CustomerDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerContact> CustomerContacts { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<Product> products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(150)");


            base.OnModelCreating(modelBuilder);
        }

       
    }
}
//public CustomerDbContext(IConfiguration config, DbContextOptions<CustomerDbContext> options)
//    : base(options)
//{
//    _config = config ?? throw new System.ArgumentNullException(nameof(config));
//}

//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//{
//    optionsBuilder.UseSqlServer(_config["connectionString"], options =>
//    {
//        options.MigrationsHistoryTable("__UsersMigrationsHistory", "Customers");
//    });
//}

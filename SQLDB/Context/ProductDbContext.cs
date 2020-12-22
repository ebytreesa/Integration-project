using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLDB.Context
{
    public class ProductDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public ProductDbContext(IConfiguration config, DbContextOptions<ProductDbContext> options)
            : base(options)
        {
            _config = config ?? throw new System.ArgumentNullException(nameof(config));
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("SQLConnection"), options =>
            {
                options.MigrationsHistoryTable("__UsersMigrationsHistory", "Product");
            });
        }

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

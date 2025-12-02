using Dev.Talabat.Infrastructure.persistence.Data.Config.Products;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Talabat.Infrastructure.persistence.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        DbSet<Product> Products { get; set; }
        DbSet<ProductBrand> ProductBrands { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }
    }
}

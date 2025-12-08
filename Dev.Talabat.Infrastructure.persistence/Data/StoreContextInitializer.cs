using Dev.Talabat.Domain.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Dev.Talabat.Infrastructure.persistence.Data
{
    internal class StoreContextInitializer(StoreContext _storeContext, ILoggerFactory loggerFactory) : IStoreContextInitializer
    {
        public async Task InitializeAsync()
        {
            var pandingMigrations = await _storeContext.Database.GetPendingMigrationsAsync();
            if (pandingMigrations.Any())
                await _storeContext.Database.MigrateAsync();
        }

        public async Task SeedAsync()
        {
            #region seeding Brands data 
            if (!_storeContext.ProductBrands.Any())
            {
                try
                {
                    string brandsFilePath = "../Dev.Talabat.Infrastructure.persistence/Data/Seeds/brands.json";
                    var brandsData = await File.ReadAllTextAsync(brandsFilePath);
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    if (brands is not null && brands.Any())
                    {
                        await _storeContext.ProductBrands.AddRangeAsync(brands);
                        await _storeContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger("StoreContextSeed");
                    logger.LogError("Error seeding Product Brands: ");
                    logger.LogError(ex.Message);
                }
            }
            #endregion

            #region seeding categories data
            if (!_storeContext.ProductCategories.Any())
            {
                try
                {
                    var categoriesFilePath = "../Dev.Talabat.Infrastructure.persistence/Data/Seeds/categories.json";
                    var categoriesData = await File.ReadAllTextAsync(categoriesFilePath);
                    var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
                    if (categories is not null && categories.Any())
                    {
                        await _storeContext.ProductCategories.AddRangeAsync(categories);
                        await _storeContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger("StoreContextSeed");
                    logger.LogError("Error seeding Product Categories: ");
                    logger.LogError(ex.Message);
                }
            }
            #endregion

            #region seeding products data
            if (!_storeContext.Products.Any())
            {
                try
                {
                    var productsFilePath = "../Dev.Talabat.Infrastructure.persistence/Data/Seeds/products.json";
                    var productsData = await File.ReadAllTextAsync(productsFilePath);
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (products is not null && products.Any())
                    {
                        await _storeContext.Products.AddRangeAsync(products);
                        await _storeContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger("StoreContextSeed");
                    logger.LogError("Error seeding Products: ");
                    logger.LogError(ex.Message);
                }
            }
            #endregion
        }
    }
}

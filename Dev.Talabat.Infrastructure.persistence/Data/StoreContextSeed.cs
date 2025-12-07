using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Dev.Talabat.Infrastructure.persistence.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync (StoreContext dbContext, ILoggerFactory loggerFactory)
        {
            #region seeding Brands data 
            if (!dbContext.ProductBrands.Any())
            {
                try
                {
                    string brandsFilePath = "../Dev.Talabat.Infrastructure.persistence/Data/Seeds/brands.json";
                    var brandsData = await File.ReadAllTextAsync(brandsFilePath);
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    if (brands is not null && brands.Any())
                    {
                        await dbContext.ProductBrands.AddRangeAsync(brands);
                        await dbContext.SaveChangesAsync();
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
            if (!dbContext.ProductCategories.Any())
            {
                try
                {
                    var categoriesFilePath = "../Dev.Talabat.Infrastructure.persistence/Data/Seeds/categories.json";
                    var categoriesData = await File.ReadAllTextAsync(categoriesFilePath);
                    var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
                    if (categories is not null && categories.Any())
                    {
                        await dbContext.ProductCategories.AddRangeAsync(categories);
                        await dbContext.SaveChangesAsync();
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
            if (!dbContext.Products.Any())
            {
                try
                {
                    var productsFilePath = "../Dev.Talabat.Infrastructure.persistence/Data/Seeds/products.json";
                    var productsData = await File.ReadAllTextAsync(productsFilePath);
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (products is not null && products.Any())
                    {
                        await dbContext.Products.AddRangeAsync(products);
                        await dbContext.SaveChangesAsync();
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

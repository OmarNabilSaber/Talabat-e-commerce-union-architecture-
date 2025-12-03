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
                    logger.LogError("Error seeding ProductBrands: ");
                    logger.LogError(ex.Message);
                }
            }
        }
    }
}

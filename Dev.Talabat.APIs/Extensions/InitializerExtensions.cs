using Dev.Talabat.Domain.Contracts;

namespace Dev.Talabat.APIs.Extensions
{
    public static class InitializerExtensions
    {
        extension (WebApplication app)
        {
            public async Task InitializeStoreContextAsync()
            {
                

                using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;
                var storeContextInitializer = services.GetRequiredService<IStoreContextInitializer>();
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    await storeContextInitializer.InitializeAsync();
                    await storeContextInitializer.SeedAsync();
                }
                catch
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError("An error occurred during applying the migration Or data seeding");
                }
            }
        }
    }
}

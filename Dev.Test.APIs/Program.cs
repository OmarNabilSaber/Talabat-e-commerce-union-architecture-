using Dev.Talabat.Infrastructure.persistence;
using Dev.Talabat.Infrastructure.persistence.Data;
using Microsoft.EntityFrameworkCore;
namespace Dev.Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            webApplicationBuilder.Services.AddControllers();
            webApplicationBuilder.Services.AddPresistenceServices(webApplicationBuilder.Configuration);
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            // webApplicationBuilder.Services.AddOpenApi();

            // configure swagger 
            webApplicationBuilder.Services.AddSwaggerGen();
            webApplicationBuilder.Services.AddEndpointsApiExplorer();

            var app = webApplicationBuilder.Build();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var storeContext = services.GetRequiredService<StoreContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var pandingMigrations = storeContext.Database.GetPendingMigrations();
                if (pandingMigrations.Any())
                    await storeContext.Database.MigrateAsync();
            }
            catch
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError("An error occurred during applying the migration");
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                // app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

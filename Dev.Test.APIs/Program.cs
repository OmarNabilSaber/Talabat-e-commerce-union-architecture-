using Dev.Talabat.Infrastructure.persistence;
using Dev.Talabat.Infrastructure.persistence.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore;
namespace Dev.Talabat.APIs
{
    public class Program
    {
        public static void Main(string[] args)
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

using Dev.Talabat.APIs.Extensions;
using Dev.Talabat.APIs.Services;
using Dev.Talabat.Application.abstruction;
using Dev.Talabat.Domain.Contracts;
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

            #region configure services
            // Add services to the container.
            webApplicationBuilder.Services.AddControllers();
            webApplicationBuilder.Services.AddHttpContextAccessor();
            webApplicationBuilder.Services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));
            webApplicationBuilder.Services.AddPresistenceServices(webApplicationBuilder.Configuration);
            
            
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            // webApplicationBuilder.Services.AddOpenApi();

            // configure swagger 
            webApplicationBuilder.Services.AddSwaggerGen();
            webApplicationBuilder.Services.AddEndpointsApiExplorer();

            var app = webApplicationBuilder.Build();
            #endregion

            #region Databases initialization
            await app.InitializeStoreContextAsync();
            #endregion

            #region configure middlewares
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
            #endregion

            app.Run();
        }
    }
}

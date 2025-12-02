using Dev.Talabat.Infrastructure.persistence.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Talabat.Infrastructure.persistence
{
    public static class DependencyInjection
    {
        extension (IServiceCollection services)
        {
            public IServiceCollection AddPresistenceServices(IConfiguration configuration)
            {
                services.AddDbContext<StoreContext>((optionsBuilder) =>
                {
                    optionsBuilder.UseSqlServer(configuration.GetConnectionString("StoreContext"));
                });
                return services;
            }
        }
    }
}

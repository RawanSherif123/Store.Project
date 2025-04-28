using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data.Repositories;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Persistence.Identity;

namespace Persistence
{
    public  static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration["ConnectionStrings : DefaultConnection"]);
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<StoreIdentityDBContext>(options =>
            {
               
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddScoped<IDbInitializer, DbInitializer>(); // Allow DI For DbInitializer
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();

            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);
            } );
            return services;
        }
    }
}

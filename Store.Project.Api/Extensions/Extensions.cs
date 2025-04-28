using Domain.Contracts;
using Domain.Models.identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Persistence.Identity;
using Services;
using Shared.ErrorsModels;
using Store.Project.Api.MiddleWares;

namespace Store.Project.Api.Extensions
{
    public static class Extensions
    {
        

        public static IServiceCollection RegisterAllServices(this IServiceCollection services , IConfiguration configuration)
        {

            services.AddBuiltInServices();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddSwaggerServices();

            services.AddInfrastructureServices(configuration);
            services.AddIdentityServices();
            services.AddApplicationServices();



            services.ConfigureServices();


            return services;
        }

        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();
    
           
            return services;
        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser,IdentityRole>()
                    .AddEntityFrameworkStores<StoreIdentityDBContext>();

            return services;
        }


        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            return services;
        }

        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {

            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any())
                                              .Select(m => new ValidationError()
                                              {
                                                  Field = m.Key,
                                                  Errors = m.Value.Errors.Select(errors => errors.ErrorMessage)
                                              });
                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });


            return services;
        }

        public static async Task< WebApplication> ConfigurMiddlewares(this WebApplication app)
        {


           await app.InitializedDatabaseAsync();

                 app.UseGlobalErrorHandling();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }

        private static async Task<WebApplication> InitializedDatabaseAsync(this WebApplication app)
        {


            #region Seeding

            using var scope = app.Services.CreateScope();
            var dbinitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbinitializer.InitializeAsync();
            await dbinitializer.InitializeIdentityAsync();


            #endregion


            return app;
        }

        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {

            app.UseMiddleware<GlobalErrorHandlingMiddleWares>();


            return app;
        }


    }
}

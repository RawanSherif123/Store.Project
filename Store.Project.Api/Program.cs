
using System.Runtime.InteropServices;
using Domain.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Data.Repositories;
using Services;
using Services.Abstractions;
using Shared.ErrorsModels;
using Store.Project.Api.Extensions;
using Store.Project.Api.MiddleWares;
using AssemblyMapping = Services.AssemblyReference;
namespace Store.Project.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.RegisterAllServices(builder.Configuration);


            var app = builder.Build();




           await app.ConfigurMiddlewares();



            app.Run();
        }
    }
}

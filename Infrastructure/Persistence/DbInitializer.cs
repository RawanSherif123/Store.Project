using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;

        public DbInitializer(StoreDbContext context)
        {
            _context = context;
        }
        public async Task InitializeAsync()
        {
            // Create Database If it doesn't Exists && Apply to any Pending Migrations 
            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync();
                }
                // Data Seeding 

                if (!_context.ProductTypes.Any())
                {
                    // Seeding ProductTypes  From Json Files 
                    // 1. Read All Data From types Json File as string 
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");
                    // 2. Transform to C# Object [List<ProductTypes>]
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    // 3. Add List<ProductTypes> To Database 
                    if (types is not null && types.Any())
                    {
                        await _context.ProductTypes.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }

                }

                // Seeding ProductBrands From Json Files 

                if (!_context.ProductBrands.Any())
                {
                    // Seeding ProductTypes  From Json Files 
                    // 1. Read All Data From brands Json File as string 
                    var BrandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");
                    // 2. Transform to C# Object [List<ProductTypes>]
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                    // 3. Add List<ProductTypes> To Database 
                    if (brands is not null && brands.Any())
                    {
                        await _context.ProductBrands.AddRangeAsync(brands);
                        await _context.SaveChangesAsync();
                    }

                }
                // Seeding Products  From Json Files 
                if (!_context.Products.Any())
                {
                    // Seeding Products  From Json Files 
                    // 1. Read All Data From  products Json File as string 
                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");
                    // 2. Transform to C# Object [List<ProductTypes>]
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    // 3. Add List<ProductTypes> To Database 
                    if (products is not null && products.Any())
                    {
                        await _context.Products.AddRangeAsync(products);
                        await _context.SaveChangesAsync();
                    }

                }
            }
            catch ( Exception)
            {

                throw;
            }


        }
    }
}


// ..\Infrastructure\Persistence\Data\Seeding\types.json
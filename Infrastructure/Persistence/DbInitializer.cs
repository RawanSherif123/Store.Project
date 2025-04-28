using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Domain.Models.identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Persistence.Data;
using Persistence.Identity;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;
        private readonly StoreIdentityDBContext _identityDBContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(StoreDbContext context , 
            StoreIdentityDBContext IdentityDBContext,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _identityDBContext = IdentityDBContext;
            _userManager = userManager;
            _roleManager = roleManager;
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

        //public async Task InitializeIdentityAsync()
        //{
        //    if (_identityDBContext.Database.GetPendingMigrations().Any())
        //    {
        //       await _identityDBContext.Database.MigrateAsync();
        //    }

        //    if (!_roleManager.Roles.Any())
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole()
        //        {
        //            Name = "Admin"
        //        });
        //        await _roleManager.CreateAsync(new IdentityRole()
        //        {
        //            Name = "SuperAdmin"
        //        });
        //    }

        //    if (! _userManager.Users.Any())
        //    {
        //        var superAdminUser = new AppUser()
        //        {
        //            DisplayName = "Super Admin",
        //            Email = "SuperAdmin@gmail.com",
        //            UserName = "SuperAdmin",
        //            PhoneNumber = "01234567890"

        //        };


        //        var AdminUser = new AppUser()
        //        {
        //            DisplayName = "Admin",
        //            Email = "Admin@gmail.com",
        //            UserName = "Admin",
        //            PhoneNumber = "01234567890"

        //        };

        //       await _userManager.CreateAsync(superAdminUser,"P@ssw0rd");
        //       await _userManager.CreateAsync(AdminUser, "P@ssw0rd");

        //       await   _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
        //       await _userManager.AddToRoleAsync(AdminUser, "Admin");

        //    }
        //}

        public async Task InitializeIdentityAsync()
        {
            var roles = new[] { "SuperAdmin", "Admin" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var superAdminUser = new AppUser
            {
                DisplayName = "Super Admin",
                Email = "SuperAdmin@gmail.com",
                UserName = "SuperAdmin",
                PhoneNumber = "01234567890"
            };

            var adminUser = new AppUser
            {
                DisplayName = "Admin",
                Email = "Admin@gmail.com",
                UserName = "Admin",
                PhoneNumber = "01234567890"
            };

            if (await _userManager.FindByEmailAsync(superAdminUser.Email) == null)
            {
                var result = await _userManager.CreateAsync(superAdminUser, "P@ssword123!");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                }
            }

            if (await _userManager.FindByEmailAsync(adminUser.Email) == null)
            {
                var result = await _userManager.CreateAsync(adminUser, "P@ssword123!");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }

    }
}


// ..\Infrastructure\Persistence\Data\Seeding\types.json
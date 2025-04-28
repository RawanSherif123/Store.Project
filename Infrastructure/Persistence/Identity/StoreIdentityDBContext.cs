
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Identity
{
    public class StoreIdentityDBContext(DbContextOptions<StoreIdentityDBContext> options ): IdentityDbContext<AppUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); 
            builder.Entity<Address>().ToTable("Addresses");
        }
    }
}

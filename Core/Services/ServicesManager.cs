using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;

namespace Services
{
    public class ServicesManager(IUnitOfWork unitOfWork ,
        IMapper mapper ,
        IBasketRepository basketRepository ,
        ICacheRepository  cacheService ,
        UserManager<AppUser> userManager) 
        : IServicesManager
    {
        public IProductService productService { get; } = new ProductService (unitOfWork , mapper);

        public IBasketService BasketService { get; } = new BasketService(basketRepository, mapper);

        public ICacheService CacheService { get; } = new CacheService(cacheService) ;

        public IAuthService AuthService { get; } = new AuthService(userManager);
    }
}

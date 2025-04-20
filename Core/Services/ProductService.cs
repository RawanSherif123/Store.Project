using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Services.Sepcifications;
using Shared;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {

        public async Task<IEnumerable<ProductResultDto>> GetAllProductAsync()
        {
            var sepc = new ProductWithBrandsAndTypesSepcifications();
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(sepc);
            var result = mapper.Map<IEnumerable<ProductResultDto>>(products);
            return result;
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var sepc = new ProductWithBrandsAndTypesSepcifications();

            var product = await unitOfWork.GetRepository<Product, int> ().GetAsync(sepc);
            if (product is null) return null;
          var result =  mapper .Map<ProductResultDto>(product);
            return result;
        }
        public async Task<IEnumerable<BrandsResultDto>> GetProductBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
           var result = mapper.Map<IEnumerable<BrandsResultDto>>(brands);
            return result;
        }

       

        public async Task<IEnumerable<TypeResultDto>> GetProductTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<TypeResultDto>>(types);
            return result;
        }

       
    }

}

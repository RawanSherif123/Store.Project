using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstractions;
using Services.Sepcifications;
using Shared;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {

        public async Task<PaginationResponse<ProductResultDto>> GetAllProductAsync(ProductSepcificationsParamters specParams)
        {
            var sepc = new ProductWithBrandsAndTypesSepcifications(specParams);
            
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(sepc);

            var speccount = new ProductWithCountSepcifications(specParams);

            var count  = await unitOfWork.GetRepository<Product,int>().CountAsync(speccount);
           // var count = products.Count();


             var result = mapper.Map<IEnumerable<ProductResultDto>>(products);
            return new PaginationResponse<ProductResultDto>(specParams.PageIndex,specParams.pageSize,count,result);
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var sepc = new ProductWithBrandsAndTypesSepcifications(id);

            var product = await unitOfWork.GetRepository<Product, int> ().GetAsync(sepc);
            if (product is null) throw new ProductNotFoundExceptions(id);
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

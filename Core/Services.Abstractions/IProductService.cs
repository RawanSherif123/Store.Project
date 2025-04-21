using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Services.Abstractions
{
    public interface IProductService
    {
        // Get All Product
        //  Task<IEnumerable<ProductResultDto>> GetAllProductAsync(int? brandId, int? typeId , string? sort , int pageIndex = 1, int pageSize = 5);
        Task<PaginationResponse<ProductResultDto>> GetAllProductAsync(ProductSepcificationsParamters specParams);

        // Get All By Id 
        Task<ProductResultDto?> GetProductByIdAsync(int id);

        // Get All By Brands  
        Task<IEnumerable<BrandsResultDto>> GetProductBrandsAsync();

        // Get All By Types 
        Task<IEnumerable<TypeResultDto>> GetProductTypesAsync();



    }
}

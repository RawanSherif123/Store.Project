using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Sepcifications
{
    public class ProductWithBrandsAndTypesSepcifications : BaseSepcifications <Product,int>
    {
        public ProductWithBrandsAndTypesSepcifications(int id ) : base(p => p.Id == id )
        {
            ApplyIncluds();
        }

        public ProductWithBrandsAndTypesSepcifications(ProductSepcificationsParamters specParams)
            : base(
                     P =>
                     (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search.ToLower()))&&
                     (!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId) &&
                     (!specParams.TypeId.HasValue || P.TypeId == specParams.TypeId)
                   )
        {
            ApplyIncluds();
            ApplySorting(specParams.Sort);
            ApplyPagination(specParams.PageIndex, specParams.pageSize);

        }

        private void ApplyIncluds()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

        private void ApplySorting (string?  sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {

                    case "namedesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;

                    default:
                        AddOrderBy(p => p.Name);
                        break;


                }
            }
            else
            {
                AddOrderBy(p => p.Name);

            }
        }
    }
}

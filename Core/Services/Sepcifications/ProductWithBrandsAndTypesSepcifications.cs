using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Services.Sepcifications
{
    public class ProductWithBrandsAndTypesSepcifications : BaseSepcifications <Product,int>
    {
        public ProductWithBrandsAndTypesSepcifications(int id ) : base(p => p.Id == id )
        {
            ApplyIncluds();
        }

        public ProductWithBrandsAndTypesSepcifications() : base (null)
        {
            ApplyIncluds();
        }

        private void ApplyIncluds()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}

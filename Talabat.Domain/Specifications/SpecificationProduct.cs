using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;

namespace Talabat.Domain.Specifications
{
    public class SpecificationProduct : BaseSpecifications<Product>
    {
        public SpecificationProduct(SpecificationParams specParams):base( P=> 
        (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search)) &&
        (!specParams.brandId.HasValue || P.ProductBrandId == specParams.brandId) 
        && (!specParams.typeId.HasValue || P.ProductTypeId == specParams.typeId)
        )
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(p => p.ProductType);
            if (!string.IsNullOrEmpty(specParams.sort))
            {
                switch (specParams.sort)
                {
                    case "priceAsc":
                        OrderBy = P => P.Price;
                        break;
                    case "priceDesc":
                        OrderByDesc = P => P.Price;
                        break;
                    default:
                        OrderBy = P => P.Name;
                        break;
                }
            }
            else
                OrderBy = P => P.Name;

            ApplyPagination(specParams.PageSize * (specParams.Index - 1), specParams.PageSize);
        }
        public SpecificationProduct(int id) : base(P=> P.Id == id) 
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(p => p.ProductType);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;

namespace Talabat.Domain.Specifications
{
    public class GetCountSpecificationWithFilteration : BaseSpecifications<Product>
    {
        public GetCountSpecificationWithFilteration(SpecificationParams specParams):base(P=>
        (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search)) &&
        (!specParams.brandId.HasValue || P.ProductBrandId == specParams.brandId) 
        && (!specParams.typeId.HasValue || P.ProductTypeId == specParams.typeId)
        )
        {

        }
    }
}

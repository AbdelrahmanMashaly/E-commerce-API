using AutoMapper;
using Talabat.APIs.Helpers;
using Talabat.Domain.Entities;
using Talabat.Domain.Entities.Identity;

namespace Talabat.APIs.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product,ProductToReturnDto>().ForMember(d=>d.ProductBrand,O=>O.MapFrom(s=>s.ProductBrand.Name))
                .ForMember(d=>d.ProductType,O=>O.MapFrom(s=>s.ProductType.Name))
                .ForMember(d=>d.PictureUrl,O=>O.MapFrom<ProductPictureResolver>());

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
        }
    }
}

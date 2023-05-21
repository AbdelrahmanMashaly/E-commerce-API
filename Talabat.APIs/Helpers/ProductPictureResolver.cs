using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Domain.Entities;

namespace Talabat.APIs.Helpers
{
    public class ProductPictureResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{configuration["APIbaseURL"]}{source.PictureUrl}";

            return string.Empty;
        }
    }
}

  

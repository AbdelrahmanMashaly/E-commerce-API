using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Domain.Entities;
using Talabat.Domain.IRepository;
using Talabat.Domain.Specifications;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductType> typeRepo,
            IGenericRepository<ProductBrand> brandRepo,
            IMapper mapper)
        {
            _productsRepo = productsRepo;
            _typeRepo = typeRepo;
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagintionHelper<ProductToReturnDto>>> GetAllProducts([FromQuery] SpecificationParams specParams)
        {
            var spec = new SpecificationProduct(specParams); 
           var result = await _productsRepo.GetAllWithSpec(spec);

            var test = new GetCountSpecificationWithFilteration(specParams);
            var count = await _productsRepo.GetCountwithSpecAsync(test);

            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(result);
            return Ok(new PagintionHelper<ProductToReturnDto>(specParams.Index , specParams.PageSize,count, Data));
        }
        [ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        [HttpGet("{id}")]

        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new SpecificationProduct(id);
            var result = await _productsRepo.GetByIdwithSpec(spec);
            if (result is null)
                return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product,ProductToReturnDto>(result));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var result = await _brandRepo.GetAll();
            return Ok(result);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllTypes()
        {
            var result = await _typeRepo.GetAll();
            return Ok(result);
        }
    }
}

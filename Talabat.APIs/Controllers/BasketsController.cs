using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Domain.Entities;
using Talabat.Domain.IRepository;

namespace Talabat.APIs.Controllers
{
    
    public class BasketsController : BaseAPIController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper mapper;

        public BasketsController(IBasketRepository basketRepository,IMapper mapper) {
            _basketRepository = basketRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetCutomerBasket(string Id)
        {
         var basket = await _basketRepository.GetBasketAsync(Id);
         return basket is null ? new CustomerBasket(Id) : basket;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto customerBasketdto)
        {
            var mappedBasket = mapper.Map<CustomerBasketDto, CustomerBasket>(customerBasketdto);
            var createdOrUpdated = await _basketRepository.UpdateBasket(mappedBasket);
            if (createdOrUpdated is null) return BadRequest(new ApiResponse(400));
            return Ok(createdOrUpdated);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string Id)
        {
            return await _basketRepository.DeleteBasketAsync(Id);
        }

    }
}

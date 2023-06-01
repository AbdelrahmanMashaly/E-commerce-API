using System.ComponentModel.DataAnnotations;
using Talabat.Domain.Entities;

namespace Talabat.APIs.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }

        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}

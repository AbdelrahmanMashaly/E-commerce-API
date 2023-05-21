using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;

namespace Talabat.Domain.IRepository
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string Id);

        Task<CustomerBasket?> UpdateBasket(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string Id);
    }
}

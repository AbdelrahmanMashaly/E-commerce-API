using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;
using Talabat.Domain.IRepository;
using StackExchange.Redis;
using System.Text.Json;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string Id)
        {
            return await _database.KeyDeleteAsync(Id);
        }

        public async Task<CustomerBasket> GetBasketAsync(string Id)
        {
            var basket = await _database.StringGetAsync(Id);
            return basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateBasket(CustomerBasket basket)
        {
            var CreatedOrUpdated = await _database.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket), TimeSpan.FromDays(1));
            if (CreatedOrUpdated is false) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
